using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Project
{
    public class GetProjectDetailResponse
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public ProjectEnum.ProjectStatus ProjectStatus { get; set; }
        public int ProjectParticipations { get; set; }
        public decimal ProjectBonus { get; set; }
        public ICollection<GetEmployeeProjectResponse>? EmployeeProjects { get; set; }
    }
}
