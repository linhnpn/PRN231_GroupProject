using AutoMapper;
using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.Enums;
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
        private IEmailSenderService _emailSender;

        public EmployeeRepository(IUnitOfWork unitOfWork, IMapper mapper, IJWTServices jWTServices, IEmailSenderService emailSender)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
            this.jWTServices = jWTServices;
            this._emailSender = emailSender;
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

                if (employee.EmployeeStatus == (int)EmployeeStatus.Inactive)
                {
                    throw new UnauthorizedException("The account is in an inaccessible state.");
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

        public async Task<List<GetEmployeeResponse>> GetListEmployeeResponseAsync(int projectId)
        {
            try
            {
                List<Employee> employees = await this._unitOfWork.EmployeeDAO.GetEmployeesByProjectIDAsync(projectId);
                return this._mapper.Map<List<GetEmployeeResponse>>(employees);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetListEmployeeResponseIDandName>> GetListEmployeeResponseIDandNameAsync(int projectId)
        {
            try
            {
                List<Employee> employees = await this._unitOfWork.EmployeeDAO.GetEmployeeByProjectIDAsync(projectId);
                return this._mapper.Map<List<GetListEmployeeResponseIDandName>>(employees);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetListEmployeeResponseIDandName>> GetListEmployeeResponseIDandNameNotInAnyProjectAsync()
        {
            try
            {
                List<Employee> employees = await this._unitOfWork.EmployeeDAO.GetEmployeeNoAnyProjectAsync();
                return this._mapper.Map<List<GetListEmployeeResponseIDandName>>(employees);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetListEmployeeResponseIDandName>> GetListEmployeeResponseNoPayRollAsync()
        {
            try
            {
                List<Employee> employees = await this._unitOfWork.EmployeeDAO.GetAllEmployeeNoPayRollAsync();
                return this._mapper.Map<List<GetListEmployeeResponseIDandName>>(employees);
            }
            catch (Exception ex)
            {
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
        public async Task<int> CreateEmployee(CreateEmployeeRequest employeeRequest)
        {
            try
            {
                var employeeCheck = await this._unitOfWork.EmployeeDAO.GetEmployeeByUsername(employeeRequest.UserName);
                if (employeeCheck != null) throw new BadRequestException("The username is duplicated.");
                await _emailSender.SendEmailAsync(employeeRequest.EmailAddress,
                    "Your account have been create", "You was created a account from Admin!\r\nusername : " + employeeRequest.UserName + "\r\npassword: " + employeeRequest.Password);
                return await this._unitOfWork.EmployeeDAO.CreateEmployee(employeeRequest);
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
                if (ex.Message.Contains("The username is duplicated."))
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            return await this._unitOfWork.EmployeeDAO.GetAllEmployee();
        }

        public async Task<Employee> UpdateEmployee(UpdateEmployeeRequest updateEmployee)
        {
            try
            {
                var employeeCheck = await this._unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(updateEmployee.EmployeeID);
                if (employeeCheck == null) throw new NotFoundException("The employeeID is not found.");
                return await this._unitOfWork.EmployeeDAO.UpdateEmployee(updateEmployee);
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
                if (ex.Message.Contains("The employeeID is not found."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> UpdateStatusEmployee(UpdateStatusEmployeeRequest employeeRequest)
        {
            try
            {
                var employeeCheck = await this._unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(employeeRequest.EmployeeId);
                if (employeeCheck == null) throw new NotFoundException("The employeeID is not found.");
                return await this._unitOfWork.EmployeeDAO.UpdateStatusEmployee(employeeRequest);
            }
            catch (Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();
                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };
                if (ex.Message.Contains("The employeeID is not found."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                errors.Add(error);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetALLEmployeeNotStart()
        {
            try
            {
                var employee = await this._unitOfWork.EmployeeDAO.GetAllEmployeeNotStart();
                if (employee.Count == 0) throw new NotFoundException("There are currently no employees not starting.");
                return employee;
            }
            catch (Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();
                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };
                if (ex.Message.Contains("There are currently no employees not starting."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                errors.Add(error);
                throw new Exception(ex.Message);
            }
        }
    }
}
