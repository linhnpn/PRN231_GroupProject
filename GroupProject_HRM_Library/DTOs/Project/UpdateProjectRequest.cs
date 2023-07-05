using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProject_HRM_Library.Enums;

namespace GroupProject_HRM_Library.DTOs.Project
{
    public class UpdateProjectRequest
    {
        [Required(ErrorMessage = "Project Name is required")]
        [DisplayName("Project Name")]
        [StringLength(200, ErrorMessage = "Project Name must have length from {2} to {1} characters", MinimumLength = 1)]
        public string? ProjectName { get; set; }
        [Required(ErrorMessage = "Project Description is required")]
        [DisplayName("Project Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Project Name must have length from {2} to {1} characters", MinimumLength = 1)]
        public string? ProjectDescription { get; set; }
        [Required(ErrorMessage = "Project Status is required")]
        [DisplayName("Project Status")]
        public int ProjectStatus { get; set; }
        [Required(ErrorMessage = "Project Bonus is required")]
        [DisplayName("Project Bonus")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Project Bonus range from {2} to {1} dolars.")]
        public decimal ProjectBonus { get; set; }
    }
}
