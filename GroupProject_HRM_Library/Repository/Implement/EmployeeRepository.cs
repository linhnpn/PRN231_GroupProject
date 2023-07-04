using AutoMapper;
using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using GroupProject_HRM_Library.Services;
using Newtonsoft.Json;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IJWTServices jWTServices;

        public EmployeeRepository(IUnitOfWork unitOfWork, IMapper mapper, IJWTServices jWTServices)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
            this.jWTServices = jWTServices;
        }

        public async Task<AuthenResponse> Authenticate(AuthenRequest authenRequest)
        {
            try
            {
                var employee = await this._unitOfWork.EmployeeDAO.Authenticate(authenRequest.Username, authenRequest.Password);

                if (employee == null)
                {
                    throw new UnauthorizedException("Username or Password is invalid.");
                }

                var accessToken = jWTServices.GenerateJWTToken(employee.EmployeeID, employee.UserName, employee.Role.RoleName);
                AuthenResponse authenResponse = new AuthenResponse()
                {
                    EmployeeID = employee.EmployeeID,
                    RoleName = employee.Role.RoleName,
                    AccessToken = accessToken
                };
                return authenResponse;

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
                if (ex.Message.Contains("Username or Password is invalid."))
                {
                    throw new UnauthorizedException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetProfileResponse> GetProfileEmplAsync(int id)
        {
            try
            {
                Employee employee = await this._unitOfWork.EmployeeDAO.GetProfileByIDAsync(id);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }
                return this._mapper.Map<GetProfileResponse>(employee);
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
