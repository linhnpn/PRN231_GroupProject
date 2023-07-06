using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject_HRM_Library.DAO
{
    public class EmployeeProjectDAO
    {
        private HumanResourceManagementContext _dbContext;
        public EmployeeProjectDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<EmployeeProject> GetLastProjectJoinedByEmplIDAsync(int id)
        {
            try
            {
                return await this._dbContext.EmployeeProjects.OrderBy(x => x.EndDate).LastOrDefaultAsync(x => x.EmployeeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeProject> AssignEmployeeToProject(AssignEmployeeToProjectRequest assignRequest)
        {
            try
            {
                EmployeeProject employeeProject = new EmployeeProject()
                {
                    EmployeeID = assignRequest.EmployeeId,
                    ProjectID = assignRequest.ProjectId,
                    StartDate = assignRequest.StartDate,
                    EndDate = assignRequest.EndDate,
                    EmployeeProjectStatus = (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress,
                };
                _dbContext.EmployeeProjects.Add(employeeProject);
                _dbContext.SaveChanges();
                return employeeProject;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeProject> GetEmployeeProjectWorkInProgress(int employeeID)
        {
            try
            {
                return _dbContext.EmployeeProjects.Where(x => x.EmployeeID == employeeID && x.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress).FirstOrDefault();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeProject> GetManagerProjectWorkInProgress()
        {
            try
            {
                return _dbContext.EmployeeProjects.Include(p => p.Employee)
                    .Where(x => x.Employee.RoleID == (int)EmployeeRole.Manager && x.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
