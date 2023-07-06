using AutoMapper;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class NotificationRepository : INotificationRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public NotificationRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Notification>> GetNotiEmplAsync(int id)
        {
            try
            {
                return await this._unitOfWork.NotificationDAO.GetNotificationEmplAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
