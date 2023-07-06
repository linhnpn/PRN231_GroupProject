using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.OvertimeLog
{
    public class OvertimeLogRequest
    {
        [Required]
        public DateTime OverTimeDate { get; set; }
        [Required]
        public int Hours { get; set; }
        [Required]
        public int OvertimeLogStatus { get; set; }
        public string? Description { get; set; }
        [Required]
        public int EmployeeID { get; set; }
    }
}
