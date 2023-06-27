using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.LeaveLog
{
    public class UpdateLeaveLogRequest
    {
        public int LeaveLogID { get; set; }
        public string? Reason { get; set; }
        public IFormFile? File { get; set; }
        public int LeaveLogStatus { get; set; }
        public string? RejectReson { get; set; }
    }
}
