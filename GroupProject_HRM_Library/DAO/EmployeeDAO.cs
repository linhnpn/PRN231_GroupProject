using Google.Cloud.Storage.V1;
using GroupProject_HRM_Library.DTOs.Employee;
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

        public async Task<Employee> GetEmployeeByIDAndProjectAsync(int id)
        {
            try
            {
                return await _dbContext.Employees
                    .Where(x => x.EmployeeProjects.Any(e => e.EmployeeID == id && e.EmployeeProjectStatus != (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress))
                    .FirstOrDefaultAsync(x => x.EmployeeID == id);
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
                var employee = _dbContext.Employees.Where(em => em.UserName.Equals(username))
                    .Include(x => x.Role)
                    .FirstOrDefault();
                if (employee != null)
                {
                    bool isMatch = BCrypt.Net.BCrypt.Verify(password, employee.Password);
                    if (isMatch)
                    {
                        return employee;
                    }
                }
                return null;
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
                                .Include(x => x.EmployeeProjects)
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

        public async Task<int> CreateEmployee(CreateEmployeeRequest employeeCreate)
        {
            try
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(employeeCreate.Password, salt);
                Employee employee = new Employee()
                {
                    EmailAddress = employeeCreate.EmailAddress,
                    EmployeeName = employeeCreate.EmployeeName,
                    EmployeeImage = employeeCreate.EmployeeImage,
                    EmployeeStatus = (int)EmployeeStatus.Active,
                    Address = employeeCreate.Address,
                    Gender = employeeCreate.Gender,
                    PhoneNumber = employeeCreate.PhoneNumber,
                    BirthDate = employeeCreate.BirthDate,
                    UserName = employeeCreate.UserName,
                    Password = hashedPassword,
                    RoleID = employeeCreate.RoleID,
                };
                _dbContext.Employees.Add(employee);
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> UpdateEmployee(UpdateEmployeeRequest updateEmployee)
        {
            try
            {
                var employee = _dbContext.Employees.FirstOrDefault(x => x.EmployeeID == updateEmployee.EmployeeID);
                employee.EmailAddress = updateEmployee.EmailAddress;
                employee.EmployeeName = updateEmployee.EmployeeName;
                employee.EmployeeImage = updateEmployee.EmployeeImage;
                employee.EmployeeStatus = updateEmployee.EmployeeStatus;
                employee.Address = updateEmployee.Address;
                employee.Gender = updateEmployee.Gender;
                employee.PhoneNumber = updateEmployee.PhoneNumber;
                employee.BirthDate = updateEmployee.BirthDate;
                employee.RoleID = updateEmployee.RoleID;
                _dbContext.Employees.Update(employee);
                _dbContext.SaveChanges();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> UpdateStatusEmployee(UpdateStatusEmployeeRequest employeeRequest)
        {
            try
            {
                var employee = _dbContext.Employees.FirstOrDefault(x => x.EmployeeID == employeeRequest.EmployeeId);
                employee.EmployeeStatus = employeeRequest.EmployeeStatus;
                _dbContext.Employees.Update(employee);
                _dbContext.SaveChanges();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> GetEmployeeByUsername(string username)
        {
            try
            {
                return _dbContext.Employees.Where(em => em.UserName.Equals(username))
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            try
            {
                return _dbContext.Employees.Where(e => e.RoleID != (int)EmployeeRole.Admin).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetEmployeeNoAnyProjectAsync()
        {
            try
            {
                var result = await _dbContext.Employees
                                .Where(x => (!x.EmployeeProjects.Any() || x.EmployeeProjects.Any(p => p.EmployeeProjectStatus != (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress))
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
        public async Task<List<Employee>> GetAllEmployeeNotStart()
        {
            try
            {
                return _dbContext.Employees
                    .Include(e => e.EmployeeProjects)
                    .Where(e => e.RoleID != (int)EmployeeRole.Admin 
                            && e.EmployeeProjects
                                    .Where(e => e.EmployeeProjectStatus == (int)EmployeeProjectEnum.EmpProStatus.WorkInProgress).ToList().Count == 0
                            && e.EmployeeStatus == (int)EmployeeStatus.Active)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
