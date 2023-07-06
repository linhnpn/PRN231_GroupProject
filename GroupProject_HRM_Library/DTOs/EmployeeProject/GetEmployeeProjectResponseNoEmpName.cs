using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.EmployeeProject
{
    public class GetEmployeeProjectResponseNoEmpName
    {
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime StartDateEmployeeProject { get; set; }
        public DateTime EndDateEmployeeProject { get; set; }
        public int EmployeeProjectStatus { get; set; }
    }
}
