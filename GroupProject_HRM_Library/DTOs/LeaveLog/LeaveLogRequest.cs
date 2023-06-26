using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.LeaveLog
{
    public class LeaveLogRequest
    {
        public string Reason { get; set; }
        public string RejectReson { get; set; }
        public int EmployeeID { get; set; }
        public IFormFile? File { get; set; }
    }
}
