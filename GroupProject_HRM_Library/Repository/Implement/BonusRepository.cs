using AutoMapper;
using GroupProject_HRM_Library.DTOs.Bonus;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class BonusRepository : IBonusRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BonusRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }

        public async Task CreateBonusAsync(BonusRequest request)
        {
            try
            {
                Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(request.EmployeeID);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }

                Bonus bonus = _mapper.Map<Bonus>(request);

                await _unitOfWork.BonusDAO.CreateBonusAsync(bonus);
                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();

                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };

                errors.Add(error);
                if (ex.Message.Contains("The employee does not exist in the system."))
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }
    }
}
