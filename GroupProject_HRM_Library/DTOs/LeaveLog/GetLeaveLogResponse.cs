using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.LeaveLog
{
    public class GetLeaveLogResponse
    {
        public int LeaveLogID { get; set; }
        public DateTime Date { get; set; }
        public string? Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? LinkProof { get; set; }
        public int LeaveLogStatus { get; set; }
        public string? RejectReson { get; set; }
        public int EmployeeID { get; set; }
        public GetEmployeeResponse? Employee { get; set; }
    }
}
