using AutoMapper;
using GroupProject_HRM_Library.Constaints;
using GroupProject_HRM_Library.DTOs.OvertimeLog;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using Newtonsoft.Json;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class OvertimeLogRepository : IOvertimeLogRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public OvertimeLogRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateOvertimeLogRequestAsync(OvertimeLogRequest request)
        {
            try
            {
                Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(request.EmployeeID);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }
                List<OvertimeLog> overtimeLogs = await _unitOfWork.OvertimeLogDAO.GetOvertimesAsync(request.EmployeeID, request.OverTimeDate);
                if (overtimeLogs.Count > 0)
                {
                    throw new BadRequestException("Already have OT in that day.");
                }
                OvertimeLog overtimeLog = _mapper.Map<OvertimeLog>(request);
                overtimeLog.LogDate = DateTime.Now;

                overtimeLog.OvertimeLogStatus = (int)OvertimeLogEnum.Status.WAITING;
                await _unitOfWork.OvertimeLogDAO.CreateOvertimeLogAsync(overtimeLog);

                Notification notification = new Notification();
                notification.EmployeeID = request.EmployeeID;
                notification.NotificationDetail = Constains.NOTI_CREATE_OT_DETAILS;
                notification.Timestamp = DateTime.Now;
                notification.isRead = false;
                await _unitOfWork.NotificationDAO.CreateNotificationAsync(notification);

                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();

                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };

                errors.Add(error);
                if (ex.Message.Contains("The employee does not exist in the system.") ||
                    ex.Message.Contains("Already have OT in that day."))
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetOvertimeLogResponse> GetOvertimeLogAsync(int id)
        {
            try
            {
                OvertimeLog overtimeLog = await this._unitOfWork.OvertimeLogDAO.GetOvertimeLogAsync(id);
                if (overtimeLog == null)
                {
                    throw new NotFoundException("The overtime log does not exist in the system.");
                }
                return this._mapper.Map<GetOvertimeLogResponse>(overtimeLog);
            }
            catch (Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();

                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };

                errors.Add(error);
                if (ex.Message.Contains("The overtime log does not exist in the system."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetOvertimeLogResponse>> GetOvertimeLogResponsesByEmplIDAsync(int emplID, int? status)
        {
            try
            {
                List<OvertimeLog> OvertimeLogs = await this._unitOfWork.OvertimeLogDAO.GetOvertimeLogResponsesByEmplIDAsync(emplID, status);
                return this._mapper.Map<List<GetOvertimeLogResponse>>(OvertimeLogs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetOvertimeLogResponse>> GetOvertimeLogResponsesByProjectIDAsync(int projectID, int? status)
        {
            try
            {
                List<OvertimeLog> OvertimeLogs = await this._unitOfWork.OvertimeLogDAO.GetOvertimeLogResponsesByProjectIDAsync(projectID, status);
                return this._mapper.Map<List<GetOvertimeLogResponse>>(OvertimeLogs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetOvertimeLogResponse>> GetOvertimeLogResponsesByStatusAsync(int? status)
        {
            try
            {
                List<OvertimeLog> OvertimeLogs = await this._unitOfWork.OvertimeLogDAO.GetOvertimeLogResponsesByStatusAsync(status);
                return this._mapper.Map<List<GetOvertimeLogResponse>>(OvertimeLogs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateOvertimeLogRequestAsync(int id, UpdateOvertimeLogRequest request)
        {
            try
            {
                OvertimeLog overtimeLog = await this._unitOfWork.OvertimeLogDAO.GetOvertimeLogAsync(id);
                if (overtimeLog == null)
                {
                    throw new Exception("The overtime log does not exist in the system.");
                }
                overtimeLog.Description = request.Description;
                overtimeLog.Hours = request.Hours;
                overtimeLog.OvertimeLogStatus = request.OvertimeLogStatus;


                this._unitOfWork.OvertimeLogDAO.UpdateOvertimeLogRequestAsync(overtimeLog);
                await this._unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();

                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };

                errors.Add(error);
                if (ex.Message.Contains("The overtime log does not exist in the system."))
                {
                    throw new Exception(JsonConvert.SerializeObject(errors));
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task UpdateStatusRequestAsync(int id, int status)
        {
            try
            {
                OvertimeLog OvertimeLog = await this._unitOfWork.OvertimeLogDAO.GetOvertimeLogAsync(id);
                if (OvertimeLog == null)
                {
                    throw new Exception("The overtime log does not exist in the system.");
                }

                OvertimeLog.OvertimeLogStatus = status;

                this._unitOfWork.OvertimeLogDAO.UpdateOvertimeLogRequestAsync(OvertimeLog);
                await this._unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();

                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };

                errors.Add(error);
                if (ex.Message.Contains("The overtime log does not exist in the system."))
                {
                    throw new Exception(JsonConvert.SerializeObject(errors));
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
