﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.OvertimeLog
{
    public class OvertimeLogRequest
    {
        public DateTime OverTimeDate { get; set; }
        public int Hours { get; set; }
        public string? Description { get; set; }
        public int EmployeeID { get; set; }
    }
}
