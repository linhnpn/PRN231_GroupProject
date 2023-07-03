using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Enums
{
    public static class ProjectEnum
    {
        public enum ProjectStatus
        {
            Cancelled = 3,
            Done = 2,
            InProgress = 1,
            NotStarted = 0
        }
        public enum ProjectOrderBy
        {
            Name_Asc,
            Name_Desc,
            Bonus_Asc,
            Bonus_Desc,
            Status_Asc,
            Status_Desc,
        }
    }
}
