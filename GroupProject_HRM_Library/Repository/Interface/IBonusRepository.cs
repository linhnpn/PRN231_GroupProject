using GroupProject_HRM_Library.DTOs.Bonus;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IBonusRepository
    {
        public Task CreateBonusAsync(BonusRequest bonus);
    }
}
