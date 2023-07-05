using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Project
{
    public class GetProjectResponse
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public int ProjectStatus { get; set; }
        public int ProjectParticipations { get; set; }
        public decimal ProjectBonus { get; set; }
    }
}
