using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DAO
{
    public class ProjectDAO
    {
        private readonly HumanResourceManagementContext _dbContext;

        public ProjectDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetProjectByIDByManagerAsync(int managerId)
        {
            try
            {
                var projectId = await _dbContext.Projects
                    .Where(p => _dbContext.EmployeeProjects
                        .Any(ep => ep.ProjectID == p.ProjectID
                            && (ep.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress)
                            && ep.EmployeeID == managerId))
                    .Select(p => p.ProjectID)
                    .FirstOrDefaultAsync();

                if (projectId == 0)
                {
                    throw new Exception($"The Project with inputted ID does not exist in the System.");
                }
                return projectId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            try
            {
                return await _dbContext.Projects
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> GetProjectParticipationsCountAsync(int proId)
        {
            try
            {
                var pro = _dbContext.Projects.Where(pro => pro.ProjectID.Equals(proId));
                if (pro == null)
                {
                    throw new Exception($"The Project with inputted ID does not exist in the System.");
                }
                return await pro.SelectMany(pro => pro.EmployeeProjects).CountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Project>> GetProjectsSortedAsync(
            string? projectName,
            int? status = null,
            decimal? bonus = null,
            string? orderBy = null)
        {
            try
            {
                var proList = _dbContext.Projects.AsQueryable();
                if (projectName != null)
                {
                    proList = proList.Where(pro => pro.ProjectName.Contains(projectName));
                }
                if (bonus != null)
                {
                    proList = proList.Where(pro => pro.ProjectBonus.Equals(bonus));
                }
                if (status != null)
                {
                    proList = proList.Where(pro => pro.ProjectStatus.Equals(status));
                }
                if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrWhiteSpace(orderBy))
                {

                    switch (orderBy)
                    {
                        case "Status_Asc":
                            proList = proList.OrderBy(h => h.ProjectStatus);
                            break;
                        case "Status_Desc":
                            proList = proList.OrderByDescending(h => h.ProjectStatus);
                            break;
                        case "Bonus_Asc":
                            proList = proList.OrderBy(h => h.ProjectBonus);
                            break;
                        case "Bonus_Desc":
                            proList = proList.OrderByDescending(h => h.ProjectBonus);
                            break;
                        case "Name_Asc":
                            proList = proList.OrderBy(h => h.ProjectName);
                            break;
                        case "Name_Desc":
                            proList = proList.OrderByDescending(h => h.ProjectName);
                            break;
                    }
                }
                else
                {
                    proList = proList.OrderBy(h => h.ProjectID);
                }
                return await proList.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Project> GetProjectByIDAsync(int proId)
        {
            try
            {
                return await _dbContext.Projects
                    .Include(proj => proj.EmployeeProjects)
                    .ThenInclude(empproj => empproj.Employee)
                    .ThenInclude(emp => emp.Role)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(pro => pro.ProjectID == proId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddNewProjectAsync(Project pro)
        {
            try
            {
                await _dbContext.Projects.AddAsync(pro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProject(int proID)
        {
            try
            {
                Project? pro = _dbContext.Projects.
                    SingleOrDefault(pro => pro.ProjectID.Equals(proID));
                _dbContext.Projects.Remove(pro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProject(Project pro)
        {
            try
            {
                var existProject = _dbContext.Projects.Local.FirstOrDefault(p => p.ProjectID.Equals(pro.ProjectID));
                if (existProject != null)
                    _dbContext.Entry(existProject).State = EntityState.Detached;
                _dbContext.Entry<Project>(pro).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Project>> GetAllProjectCanAssignEmployee()
        {
            try
            {
                var existProject = _dbContext.Projects.Where(p => p.ProjectStatus == (int)ProjectEnum.ProjectStatus.InProgress).ToList();
                return existProject;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
