using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.LeaveLog
{
    public class LeaveLogManagerRequest
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string? Reason { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public int LeaveLogStatus { get; set; }
        public IFormFile? File { get; set; }
    }
}
