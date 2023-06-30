using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Enums
{
    public static class ProjectEnum
    {
        public enum Status
        {
            Cancelled = 2,
            Done = 1,
            InProgress = 0
        }
        public enum OrderBy
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
