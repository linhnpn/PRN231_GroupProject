using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.OvertimeLog
{
    public class UpdateOvertimeLogRequest
    {
        public int OvertimeID { get; set; }
        public int Hours { get; set; }
        public int OvertimeLogStatus { get; set; }
        public string? Description { get; set; }
        public int EmployeeID { get; set; }
    }
}
