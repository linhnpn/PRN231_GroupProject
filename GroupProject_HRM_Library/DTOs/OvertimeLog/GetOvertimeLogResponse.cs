using GroupProject_HRM_Library.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.OvertimeLog
{
    public class GetOvertimeLogResponse
    {
        public int OvertimeID { get; set; }
        public DateTime OverTimeDate { get; set; }
        public DateTime LogDate { get; set; }
        public int Hours { get; set; }
        public int OvertimeLogStatus { get; set; }
        public string Description { get; set; }
        public int EmployeeID { get; set; }
        public GetEmployeeResponse? Employee { get; set; }
    }
}
