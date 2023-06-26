using AutoMapper;
using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using Newtonsoft.Json;
using static GroupProject_HRM_Library.Enums.LeaveLogEnum;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class LeaveLogRepository : ILeaveLogRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IFirebaseStorageService _firebaseStorageService;

        public LeaveLogRepository(IUnitOfWork unitOfWork, IMapper mapper, IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
        }
        public async Task CreateLeaveLogRequestAsync(LeaveLogRequest request)
        {
            try
            {
                Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(request.EmployeeID);
                if (employee == null)
                {
                    throw new BadRequestException("The employee does not exist in the system.");
                }

                LeaveLog leaveLog = _mapper.Map<LeaveLog>(request);
                leaveLog.Date = DateTime.Now;

                if (request.File != null)
                {
                    leaveLog.LinkProof = await _firebaseStorageService.UploadFile(request.File);
                }

                leaveLog.LeaveLogStatus = (int)LeaveLogEnum.Status.WAITING;
                await _unitOfWork.LeaveLogDAO.CreateLeaveLogAsync(leaveLog);
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
                if (ex.Message.Contains("The employee does not exist in the system."))
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetLeaveLogResponse> GetLeaveLogAsync(int id)
        {
            try
            {
                LeaveLog leaveLog = await this._unitOfWork.LeaveLogDAO.GetLeaveLogAsync(id);
                if (leaveLog == null)
                {
                    throw new NotFoundException("The leave log does not exist in the system.");
                }
                return this._mapper.Map<GetLeaveLogResponse>(leaveLog);
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
                if (ex.Message.Contains("The leave log does not exist in the system."))
                {
                    throw new NotFoundException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetLeaveLogResponse>> GetLeaveLogResponsesByEmplIDAsync(int emplID, int? status)
        {
            try
            {
                List<LeaveLog> leaveLogs = await this._unitOfWork.LeaveLogDAO.GetLeaveLogResponsesByEmplIDAsync(emplID, status);
                return this._mapper.Map<List<GetLeaveLogResponse>>(leaveLogs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetLeaveLogResponse>> GetLeaveLogResponsesByProjectIDAsync(int projectID, int? status)
        {
            try
            {
                List<LeaveLog> leaveLogs = await this._unitOfWork.LeaveLogDAO.GetLeaveLogResponsesByProjectIDAsync(projectID, status);
                return this._mapper.Map<List<GetLeaveLogResponse>>(leaveLogs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetLeaveLogResponse>> GetLeaveLogResponsesByStatusAsync(int? status)
        {
            try
            {
                List<LeaveLog> leaveLogs = await this._unitOfWork.LeaveLogDAO.GetLeaveLogResponsesByStatusAsync(status);
                return this._mapper.Map<List<GetLeaveLogResponse>>(leaveLogs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateLeaveLogRequestAsync(int id, UpdateLeaveLogRequest request)
        {
            try
            {
                LeaveLog leaveLog = await this._unitOfWork.LeaveLogDAO.GetLeaveLogAsync(request.LeaveLogID);
                if (leaveLog == null)
                {
                    throw new Exception("The leave log does not exist in the system.");
                }
                leaveLog.Reason = request.Reason;
                leaveLog.LeaveLogStatus = request.LeaveLogStatus;
                leaveLog.RejectReson = request.RejectReson;
                if (request.File != null)
                {
                    leaveLog.LinkProof = await _firebaseStorageService.UploadFile(request.File);
                }


                this._unitOfWork.LeaveLogDAO.UpdateLeaveLogRequestAsync(leaveLog);
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
                if (ex.Message.Contains("The leave log does not exist in the system."))
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
                LeaveLog leaveLog = await this._unitOfWork.LeaveLogDAO.GetLeaveLogAsync(id);
                if (leaveLog == null)
                {
                    throw new Exception("The leave log does not exist in the system.");
                }

                leaveLog.LeaveLogStatus = status;

                this._unitOfWork.LeaveLogDAO.UpdateLeaveLogRequestAsync(leaveLog);
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
                if (ex.Message.Contains("The leave log does not exist in the system."))
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
