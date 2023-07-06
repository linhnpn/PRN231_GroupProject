using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.LeaveLog
{
    public class LeaveLogRequest
    {
        [Required(ErrorMessage ="Required")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Reason is required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name is invalid")]
        public string? Reason { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        public IFormFile? File { get; set; }
    }
}
