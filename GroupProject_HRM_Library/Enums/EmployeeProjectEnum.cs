using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Enums
{
    public class EmployeeProjectEnum
    {
        public enum EmpProStatus
        {
            Done = 2,
            WorkInProgress = 1,
            NotStarted = 0
        }
        public enum EmpProOrderBy
        {
            EmpID_Asc,
            EmpID_Desc,
            EmpName_Asc,
            EmpName_Desc,
            Role_Asc,
            Role_Desc,
            StartDate_Asc,
            StartDate_Desc,
            EndDate_Asc,
            EndDate_Desc,
            Status_Asc,
            Status_Desc,
        }
    }
}
