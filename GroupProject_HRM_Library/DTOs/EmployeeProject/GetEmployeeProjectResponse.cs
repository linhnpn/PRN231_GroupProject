using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.EmployeeProject
{
    public class GetEmployeeProjectResponse
    {
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmailAddress { get; set; }
        public string? RoleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeProjectStatus { get; set; }
    }
}
