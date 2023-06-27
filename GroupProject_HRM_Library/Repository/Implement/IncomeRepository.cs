using AutoMapper;
using GroupProject_HRM_Library.DTOs.Income;
using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class IncomeRepository : IIncomeRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public IncomeRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetIncomeEmployeeResponse>> GetIncomeEmplAsync(int id)
        {
            try
            {
                List<Income> incomes = await this._unitOfWork.IncomeDAO.GetIncomeByEmplIDAsync(id);
                return this._mapper.Map<List<GetIncomeEmployeeResponse>>(incomes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
