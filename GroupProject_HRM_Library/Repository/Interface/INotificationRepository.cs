using GroupProject_HRM_Library.DTOs.Income;
using GroupProject_HRM_Library.DTOs.Notification;
using GroupProject_HRM_Library.Models;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface INotificationRepository
    {
        public Task<Notification> GetNotiEmplAsync(int id);
    }
}
