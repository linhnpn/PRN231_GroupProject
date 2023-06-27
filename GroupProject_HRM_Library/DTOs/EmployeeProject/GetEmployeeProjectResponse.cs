﻿using GroupProject_HRM_Library.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.EmployeeProject
{
    public class GetEmployeeProjectResponse
    {
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeProjectStatus { get; set; }
        public GetProjectResponse Project { get; set; }
    }
}