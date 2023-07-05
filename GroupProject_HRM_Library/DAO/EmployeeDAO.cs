using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject_HRM_Library.DAO
{
    public class EmployeeDAO
    {
        private HumanResourceManagementContext _dbContext;
        public EmployeeDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Employee> GetEmployeeByIDAsync(int id)
        {
            try
            {
                return await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> GetProfileByIDAsync(int id)
        {
            try
            {
                var result = await _dbContext.Employees
                        .Include(x => x.EmployeeProjects)
                            .ThenInclude(ep => ep.Project)
                            .Where(x => x.EmployeeProjects.Any(p => p.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress))
                            .FirstOrDefaultAsync(x => x.EmployeeID == id);
                if (result == null)
                {
                    result = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeID == id);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> Authenticate(string username, string password)
        {
            try
            {
                return await _dbContext.Employees.Where(em => em.UserName.Equals(username) && em.Password.Equals(password))
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetEmployeeByProjectIDAsync(int projectId)
        {
            try
            {
                var result = await _dbContext.Employees
                        .Include(x => x.EmployeeProjects)
                            .ThenInclude(ep => ep.Project)
                            .Where(x => x.EmployeeProjects.Any(p => p.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress) 
                            && x.EmployeeProjects.Any(p => p.ProjectID == projectId) 
                            && x.RoleID == (int)RoleEnum.Role.EMPLOYEE)
                            .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployeeHavePayRollAsync()
        {
            try
            {
                var result = await _dbContext.Employees
                                .Join(
                                    _dbContext.Payrolls,
                                    employee => employee.EmployeeID,
                                    payroll => payroll.EmployeeID,
                                    (employee, payroll) => employee
                                )
                                .Where(x => x.EmployeeStatus == (int)EmployeeEnum.Status.CURRENT && x.Payrolls != null)
                                .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployeeNoPayRollAsync()
        {
            try
            {
                var result = await _dbContext.Employees
                                .Where(x => x.EmployeeStatus == (int)EmployeeEnum.Status.CURRENT
                                    && !_dbContext.Payrolls.Any(p => p.EmployeeID == x.EmployeeID)
                                    && x.RoleID != (int)RoleEnum.Role.ADMIN)
                                .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetEmployeesByProjectIDAsync(int projectId)
        {
            try
            {
                var result = await _dbContext.Employees
                                .Where(x => x.EmployeeProjects.Any(p => p.ProjectID == projectId)
                                    && x.EmployeeStatus != (int)RoleEnum.Role.ADMIN)
                                .OrderBy(x => x.EmployeeID)
                                .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
