using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject_HRM_Library.DAO
{
    public class TaxDAO
    {
        private HumanResourceManagementContext _dbContext;
        public TaxDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<List<Tax>> GetAllTax(int status)
        {
            try
            {
                return await this._dbContext.Taxes.Where(t => t.TaxStatus == status).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
