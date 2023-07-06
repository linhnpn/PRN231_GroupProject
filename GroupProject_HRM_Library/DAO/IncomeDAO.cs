using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DAO
{
    public class IncomeDAO
    {
        private HumanResourceManagementContext _dbContext;
        public IncomeDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Income>> GetIncomeByEmplIDAsync(int id)
        {
            try
            {
                return await _dbContext.Incomes.OrderByDescending(x => x.EndDate).Where(x => x.EmployeeID == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Income> GetLastIncomeByEmplIDAsync(int id)
        {
            try
            {
                return await _dbContext.Incomes.OrderBy(x => x.EndDate).LastOrDefaultAsync(x => x.EmployeeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateIncomeAsync(Income income)
        {
            try
            {
                await this._dbContext.Incomes.AddAsync(income);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
