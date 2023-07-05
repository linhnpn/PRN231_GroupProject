using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Project
{
    public class CreateProjectRequest
    {
        [Required(ErrorMessage = "Project Name is required")]
        [DisplayName("Project Name")]
        [StringLength(200,ErrorMessage = "Project Name must have length from {2} to {1} characters", MinimumLength = 1)]
        public string? ProjectName { get; set; }
        [Required(ErrorMessage = "Project Description is required")]
        [DisplayName("Project Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Project Name must have length from {2} to {1} characters", MinimumLength = 1)]
        public string? ProjectDescription { get; set; }
        public int ProjectStatus { get; set; }
        public decimal ProjectBonus { get; set; }
    }
}
