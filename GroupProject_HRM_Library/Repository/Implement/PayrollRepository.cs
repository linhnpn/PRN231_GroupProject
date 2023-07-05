using AutoMapper;
using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.DTOs.Payroll;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using Newtonsoft.Json;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class PayrollRepository : IPayrollRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PayrollRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
        }
        public async Task CreatePayrollRequestAsync(PayrollRequest request)
        {
            try
            {
                Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(request.EmployeeID);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }

                Payroll payroll = _mapper.Map<Payroll>(request);
                payroll.Timestamp = DateTime.Now;

                await _unitOfWork.PayrollDAO.CreateNewPayroll(payroll);
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

        public async Task<List<GetPayrollResponse>> GetPayrollResponsesByEmplIDAsync(int imployeeID)
        {
            try
            {
                Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(imployeeID);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }

                List<Payroll> payrolls = await this._unitOfWork.PayrollDAO.GetPayrollResponsesByEmplIDAsync(imployeeID);
                return this._mapper.Map<List<GetPayrollResponse>>(payrolls);
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
