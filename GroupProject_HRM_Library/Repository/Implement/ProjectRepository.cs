using AutoMapper;
using GroupProject_HRM_Library.DTOs.Project;
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
    public class ProjectRepository : IProjectRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateProjectRequestAsync(CreateProjectRequest request)
        {
            try
            {
                var pro = _mapper.Map<Project>(request);

                await _unitOfWork.ProjectDAO.AddNewProjectAsync(pro);
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
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteProjectRequestAsync(int id)
        {
            try
            {
                var proRequest = await _unitOfWork.ProjectDAO.GetProjectByIDAsync(id);

                if (proRequest == null)
                    throw new NotFoundException("The Project with inputted ID does not exist in the System.");

                _unitOfWork.ProjectDAO.DeleteProject(id);
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
                if (ex.Message.Contains("The Project with inputted ID does not exist in the System."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Project>> GetAllProjectCanAssignEmployee()
        {
            try
            {
                var projects = await _unitOfWork.ProjectDAO.GetAllProjectCanAssignEmployee();
                if(projects.Count == 0)
                {
                    throw new NotFoundException("No projects currently need employees");
                }
                return projects;
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
                if (ex.Message.Contains("The Project with inputted ID does not exist in the System."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetIdProjectBaseOnManager(int id)
        {
            try
            {
                var proRequest = await _unitOfWork.ProjectDAO.GetProjectByIDByManagerAsync(id);

                if (proRequest == 0)
                    throw new NotFoundException("The Project with inputted ID does not exist in the System.");
                return proRequest;

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
                if (ex.Message.Contains("The Project with inputted ID does not exist in the System."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetProjectDetailResponse> GetProjectResponseAsync(int id)
        {
            try
            {
                var proRequest = await _unitOfWork.ProjectDAO.GetProjectByIDAsync(id);

                if (proRequest == null)
                    throw new NotFoundException("The Project with inputted ID does not exist in the System.");
                return _mapper.Map<GetProjectDetailResponse>(proRequest);

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
                if (ex.Message.Contains("The Project with inputted ID does not exist in the System."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetProjectResponse>> GetProjectResponsesAsync()
        {
            try
            {
                List<Project> projects = await _unitOfWork.ProjectDAO.GetProjectsAsync();
                var projectvms = _mapper.Map<List<GetProjectResponse>>(projects);
                foreach (var projectvm in projectvms)
                {
                    projectvm.ProjectParticipations = await _unitOfWork.ProjectDAO.GetProjectParticipationsCountAsync(projectvm.ProjectID);
                }
                return projectvms;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetProjectResponse>> GetProjectResponsesSortedAsync(
            string? projectName, 
            decimal? bonus = null, 
            ProjectEnum.ProjectStatus? status = null, 
            ProjectEnum.ProjectOrderBy? orderBy = null)
        {
            try
            {
                int? statusInt = null;
                if (status != null)
                {
                    statusInt = (int)status;
                }
                string? orderByString = null;
                if (orderBy != null)
                {
                    orderByString = Enum.GetName((ProjectEnum.ProjectOrderBy)orderBy);
                }
                List<Project> projects = await _unitOfWork.ProjectDAO.GetProjectsSortedAsync(
                    projectName,
                    statusInt,
                    bonus,
                    orderByString);
                var projectvms = _mapper.Map<List<GetProjectResponse>>(projects);
                foreach (var projectvm in projectvms)
                {
                    projectvm.ProjectParticipations = await _unitOfWork.ProjectDAO.GetProjectParticipationsCountAsync(projectvm.ProjectID);
                }
                return projectvms;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateProjectRequestAsync(int id, UpdateProjectRequest request)
        {
            try
            {
                var projectRequest = await _unitOfWork.ProjectDAO.GetProjectByIDAsync(id);
                if (projectRequest == null)
                    throw new NotFoundException("The Project does not exist in the System.");

                projectRequest.ProjectName = request.ProjectName;
                projectRequest.ProjectDescription = request.ProjectDescription;
                projectRequest.ProjectBonus = request.ProjectBonus;
                projectRequest.ProjectStatus = request.ProjectStatus;

                _unitOfWork.ProjectDAO.UpdateProject(projectRequest);
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
                if (ex.Message.Contains("The Project does not exist in the System."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateProjectStatusRequestAsync(int id, ProjectEnum.ProjectStatus status)
        {
            try
            {
                var projectRequest = await _unitOfWork.ProjectDAO.GetProjectByIDAsync(id);
                if (projectRequest == null)
                    throw new NotFoundException("The Project does not exist in the System.");

                projectRequest.ProjectStatus = (int)status;

                _unitOfWork.ProjectDAO.UpdateProject(projectRequest);
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
                if (ex.Message.Contains("The Project does not exist in the System."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }
    }
}
