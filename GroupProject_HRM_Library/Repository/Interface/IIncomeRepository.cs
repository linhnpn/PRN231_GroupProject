using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.DTOs.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IIncomeRepository
    {
        public Task<List<GetIncomeEmployeeResponse>> GetIncomeEmplAsync(int id);
    }
}
