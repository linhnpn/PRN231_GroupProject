using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Enums
{
    public static class TaxEnum
    {
        public enum Status
        {
            InUse = 1,
            NotInUse = 0
        }
        public enum OrderBy
        {
            MinSalary_Asc,
            MinSalary_Desc,
            MaxSalary_Asc,
            MaxSalary_Desc,
            Percent_Asc,
            Percent_Desc,
            AddDate_Asc,
            AddDate_Desc,
        }
    }
}
