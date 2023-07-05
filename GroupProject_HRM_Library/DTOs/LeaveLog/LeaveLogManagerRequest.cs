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
        [Required(ErrorMessage = "Required")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveLogStatus { get; set; }
        public IFormFile? File { get; set; }
    }
}
