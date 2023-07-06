using AutoMapper;
using Google.Cloud.Storage.V1;
using GroupProject_HRM_Library.DTOs.Income;
using GroupProject_HRM_Library.DTOs.Notification;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
