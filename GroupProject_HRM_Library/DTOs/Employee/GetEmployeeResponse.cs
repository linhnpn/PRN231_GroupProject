using GroupProject_HRM_Library.DTOs.EmployeeProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Employee
{
    public class GetEmployeeResponse
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeImage { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EmployeeStatus { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime StartDateEmployeeProject { get; set; }
        public DateTime EndDateEmployeeProject { get; set; }
        public int StatusEmployeeProject { get; set; }
        public int IdOfProjectCurrent { get; set; }
        public int RoleId { get; set; }
    }
}
