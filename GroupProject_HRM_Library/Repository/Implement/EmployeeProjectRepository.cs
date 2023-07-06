using AutoMapper;
using GroupProject_HRM_Library.DTOs.EmployeeProject;
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
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public EmployeeProjectRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
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
                    var managerProject = await this._unitOfWork.EmployeeProjectDAO.GetManagerProjectWorkInProgress();
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
    }
}
