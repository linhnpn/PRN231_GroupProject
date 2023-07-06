
using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject_HRM_Library.DAO
{
    public class NotificationDAO
    {
        private HumanResourceManagementContext _dbContext;
        public NotificationDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CreateNotificationAsync(Notification notification)
        {
            try
            {
                await this._dbContext.Notifications.AddAsync(notification);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Notification>> GetNotificationEmplAsync(int emplID)
        {
            try
            {
                return await this._dbContext.Notifications
                    .OrderByDescending(x => x.Timestamp)
                    .Where(e => e.EmployeeID == emplID).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
