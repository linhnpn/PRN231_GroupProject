using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject_HRM_Library.DAO
{
    public class EmployeeProjectDAO
    {
        private HumanResourceManagementContext _dbContext;
        public EmployeeProjectDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<EmployeeProject> GetLastProjectJoinedByEmplIDAsync(int id)
        {
            try
            {
                return await this._dbContext.EmployeeProjects.OrderBy(x => x.EndDate).LastOrDefaultAsync(x => x.EmployeeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
