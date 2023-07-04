using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return _dbContext.Employees.Where(em => em.UserName.Equals(username) && em.Password.Equals(password))
                    .Include(x => x.Role)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
