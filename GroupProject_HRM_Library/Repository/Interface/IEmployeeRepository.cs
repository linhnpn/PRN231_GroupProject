using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public Task<GetProfileResponse> GetProfileEmplAsync(int id);
        public Task<AuthenResponse> Authenticate(AuthenRequest authenRequest);
        public Task<List<GetListEmployeeResponseIDandName>> GetListEmployeeResponseIDandNameAsync(int projectId);
        public Task<List<GetEmployeeResponse>> GetListEmployeeResponseAsync(int projectId);
        public Task<List<GetListEmployeeResponseIDandName>> GetListEmployeeResponseNoPayRollAsync();

        public Task<int> CreateEmployee(CreateEmployeeRequest employeeRequest);

        public Task<Employee> UpdateEmployee(UpdateEmployeeRequest updateEmployee);

        public Task<Employee> UpdateStatusEmployee(UpdateStatusEmployeeRequest employeeRequest);

        public Task<List<Employee>> GetAllEmployee();
        public Task<List<GetListEmployeeResponseIDandName>> GetListEmployeeResponseIDandNameNotInAnyProjectAsync();
        public Task<List<Employee>> GetALLEmployeeNotStart();
    }
}
