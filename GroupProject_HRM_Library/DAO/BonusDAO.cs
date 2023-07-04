using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject_HRM_Library.DAO
{
    public class BonusDAO
    {
        private HumanResourceManagementContext _dbContext;
        public BonusDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<List<Bonus>> GetListBounusByEmpID(int id)
        {
            try
            {
                return await this._dbContext.Bonuses.Where(x => x.EmployeeID == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Bonus>> GetListBounusByEmpIDAndDate(int id, DateTime start)
        {
            try
            {
                return await this._dbContext.Bonuses.Where(x => x.EmployeeID == id && x.Timestamp > start)
                                        .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateBonusAsync(Bonus bonus)
        {
            try
            {
                await this._dbContext.Bonuses.AddAsync(bonus);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
