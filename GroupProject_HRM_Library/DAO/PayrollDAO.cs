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



        public async Task<Payroll> GetPayrollByEmpID(int id)
        {
            try
            {
                return await this._dbContext.Payrolls.FirstOrDefaultAsync(x => x.EmployeeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
