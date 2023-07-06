using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Employee
{
    public class UpdateStatusEmployeeRequest
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int EmployeeStatus { get; set; }
    }
}
