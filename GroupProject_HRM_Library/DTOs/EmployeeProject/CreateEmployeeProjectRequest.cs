using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.EmployeeProject
{
    public class CreateEmployeeProjectRequest
    {
        [Required]
        public int ProjectID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public int EmployeeProjectStatus { get; set; }
    }
}
