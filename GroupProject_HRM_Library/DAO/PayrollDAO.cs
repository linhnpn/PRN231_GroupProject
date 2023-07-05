using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject_HRM_Library.DAO
{
    public class PayrollDAO
    {
        private HumanResourceManagementContext _dbContext;
        public PayrollDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateNewPayroll(Payroll payroll) {
            try
            {
                await this._dbContext.Payrolls.AddAsync(payroll);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payroll> GetPayrollByEmpID(int id)
        {
            try
            {
                return await this._dbContext.Payrolls.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync(x => x.EmployeeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Payroll>> GetPayrollResponsesByEmplIDAsync(int? imployeeID)
        {
            try
            {
                return await this._dbContext.Payrolls.Where(x => x.EmployeeID == imployeeID).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
