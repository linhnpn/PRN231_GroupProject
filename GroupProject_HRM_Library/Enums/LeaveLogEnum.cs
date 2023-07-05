using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Enums
{
    public static class LeaveLogEnum
    {
        public enum Status
        {
            WAITING = 0,
            APPROVE = 1,
            ABSENT_WITHOUT_LEAVE= -2,
            REJECT = -1,
            CANCEL = 2
        }
    }
}
