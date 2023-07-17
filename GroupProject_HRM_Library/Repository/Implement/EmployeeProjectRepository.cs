using AutoMapper;
using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using Newtonsoft.Json;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public EmployeeProjectRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }
        public async Task CreateEmployeeProjectRequestAsync(CreateEmployeeProjectRequest request)
        {
            try
            {
                Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(request.EmployeeID);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }
                Employee employeeCheckProject = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAndProjectAsync(request.EmployeeID);
                if (employeeCheckProject != null)
                {
                    throw new BadRequestException("The employee have had belong to a project.");
                }
                Project project = await _unitOfWork.ProjectDAO.GetProjectByIDAsync(request.ProjectID);
                if (project == null)
                {
                    throw new BadRequestException("The project does not exist in the system.");
                }

                EmployeeProject employeeProject = _mapper.Map<EmployeeProject>(request);

                await _unitOfWork.EmployeeProjectDAO.CreateEmployeeProjectAsync(employeeProject);
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
                if (ex.Message.Contains("The employee does not exist in the system.") 
                    || ex.Message.Contains("The project does not exist in the system.")
                    || ex.Message.Contains("The employee have had belong to a project.")
                    )
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteEmployeeProjectRequestAsync(int employeeID, int projectID)
        {
            try
            {
                Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(employeeID);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }
                Project project = await _unitOfWork.ProjectDAO.GetProjectByIDAsync(projectID);
                if (project == null)
                {
                    throw new BadRequestException("The project does not exist in the system.");
                }
                EmployeeProject employeeProject = await _unitOfWork.EmployeeProjectDAO.GetEmployeeProjectNoStatusAsync(employeeID, projectID);
                if (employeeProject == null)
                {
                    throw new BadRequestException("The employee does not exist in the project.");
                }

                _unitOfWork.EmployeeProjectDAO.DeleteEmployeeProject(employeeID, projectID);
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
                if (ex.Message.Contains("The employee does not exist in the system.")
                    || ex.Message.Contains("The project does not exist in the system.")
                    || ex.Message.Contains("The employee does not exist in the project.")
                    )
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateEmployeeProjectRequestAsync(UpdateEmployeeProjectRequest request)
        {
            try
            {
                Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(request.EmployeeID);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }
                Project project = await _unitOfWork.ProjectDAO.GetProjectByIDAsync(request.ProjectID);
                if (project == null)
                {
                    throw new BadRequestException("The project does not exist in the system.");
                }

                EmployeeProject employeeProject = _mapper.Map<EmployeeProject>(request);

                _unitOfWork.EmployeeProjectDAO.Update(employeeProject);
                _unitOfWork.Commit();

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
                if (ex.Message.Contains("The employee does not exist in the system.")
                    || ex.Message.Contains("The project does not exist in the system.")
                    )
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }
        public async Task<EmployeeProject> AssignEmployeeToProject(AssignEmployeeToProjectRequest assignRequest)
        {
            try
            {
                var employeeInfor = await this._unitOfWork.EmployeeDAO.GetProfileByIDAsync(assignRequest.EmployeeId);
                if(employeeInfor == null)
                {
                    throw new BadRequestException("Invalid employeeId.");
                }
                if(employeeInfor.RoleID == (int)EmployeeRole.Manager)
                {
                    var managerProject = await this._unitOfWork.EmployeeProjectDAO.GetManagerProjectWorkInProgress(assignRequest.ProjectId);
                    if(managerProject != null)
                    {
                        throw new BadRequestException("The project now has a manager.");
                    }
                }
                var employeeProject = await this._unitOfWork.EmployeeProjectDAO.GetEmployeeProjectWorkInProgress(assignRequest.EmployeeId);
                if (employeeProject != null)
                {
                    throw new BadRequestException("This employee is working on a project.");
                }
                return await this._unitOfWork.EmployeeProjectDAO.AssignEmployeeToProject(assignRequest);
            }
            catch(Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();

                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };
                errors.Add(error);
                if (ex.Message.Contains("This employee is working on a project.") ||
                    ex.Message.Contains("Invalid employeeId."))
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);

            }
            
        }

        public async Task<List<EmployeeProject>> GetInforOfProjects(int projectID)
        {
            try
            {
                var result = await this._unitOfWork.EmployeeProjectDAO.GetInforOfProjects(projectID);
                if(result.Count == 0)
                {
                    throw new NotFoundException("This Project doesn't have employee.");
                }
                return result;
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
                if (ex.Message.Contains("This employee is working on a project.") ||
                    ex.Message.Contains("Invalid employeeId."))
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);

            }
        }
    }
}