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
                return await this._dbContext.EmployeeProjects
                    .OrderBy(x => x.EndDate)
                    .Where(x => x.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress)
                    .LastOrDefaultAsync(x => x.EmployeeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeProject> GetEmployeeProjectAsync(int employeeId, int projectId)
        {
            try
            {
                return await this._dbContext.EmployeeProjects
                    .OrderBy(x => x.EndDate)
                    .Where(x => x.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress)
                    .FirstOrDefaultAsync(x => x.EmployeeID == employeeId && x.ProjectID == projectId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeProject> GetEmployeeProjectNoStatusAsync(int employeeId, int projectId)
        {
            try
            {
                return await this._dbContext.EmployeeProjects
                    .OrderBy(x => x.EndDate)
                    .FirstOrDefaultAsync(x => x.EmployeeID == employeeId && x.ProjectID == projectId);
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

        public async Task<EmployeeProject> GetManagerProjectWorkInProgress(int projectId)
        {
            try
            {
                return _dbContext.EmployeeProjects.Include(p => p.Employee)
                    .Where(x => x.Employee.RoleID == (int)EmployeeRole.Manager && x.ProjectID == projectId && x.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateEmployeeProjectAsync(EmployeeProject employeeProject)
        {
            try
            {
                await this._dbContext.EmployeeProjects.AddAsync(employeeProject);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteEmployeeProject(int employeeId, int projectId)
        {
            try
            {
                EmployeeProject? tax = _dbContext.EmployeeProjects.
                    FirstOrDefault(tax => tax.EmployeeID == employeeId
                    && tax.ProjectID == projectId);
                _dbContext.EmployeeProjects.Remove(tax);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(EmployeeProject employeeProject)
        {
            try
            {
                var exist = _dbContext.EmployeeProjects.FirstOrDefault(t => t.EmployeeID == employeeProject.EmployeeID && t.ProjectID == employeeProject.ProjectID);
                if (exist != null)
                {
                    exist.StartDate = employeeProject.StartDate;
                    exist.EndDate = employeeProject.EndDate;
                    exist.EmployeeProjectStatus = employeeProject.EmployeeProjectStatus;
                    _dbContext.Entry(exist).State = EntityState.Detached;
                }
                _dbContext.Entry<EmployeeProject>(employeeProject).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<EmployeeProject>> GetInforOfProjects(int projectId)
        {
            try
            {
                return _dbContext.EmployeeProjects
                    .Include(p => p.Employee)
                    .Include(x => x.Project)
                    .Where(p => p.ProjectID == projectId).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
