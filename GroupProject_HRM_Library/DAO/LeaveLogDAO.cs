﻿using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DAO
{
    public class LeaveLogDAO
    {
        private HumanResourceManagementContext _dbContext;
        public LeaveLogDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateLeaveLogAsync(LeaveLog leaveLog)
        {
            try
            {
                await this._dbContext.LeaveLogs.AddAsync(leaveLog);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LeaveLog> GetLeaveLogAsync(int id)
        {
            try
            {
                return await this._dbContext.LeaveLogs
                                            .Include(x => x.Employee)
                                            .FirstOrDefaultAsync(x => x.LeaveLogID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LeaveLog>> GetLeaveLogResponsesByEmplIDAsync(int emplID, int? status)
        {
            try
            {
                if (status == null)
                {
                    return await this._dbContext.LeaveLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.EmployeeID == emplID)
                                            .OrderByDescending(x => x.Date)
                                            .ToListAsync();
                }
                return await this._dbContext.LeaveLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.EmployeeID == emplID && x.LeaveLogStatus == status)
                                            .OrderByDescending(x => x.Date)
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LeaveLog>> GetLeaveLogResponsesByEmplIDAndDateAsync
                                            (int emplID, int? status, DateTime start, DateTime end)
        {
            try
            {
                if (status == null)
                {
                    return await this._dbContext.LeaveLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.EmployeeID == emplID && x.Date > start && x.Date < end)
                                            .ToListAsync();
                }
                return await this._dbContext.LeaveLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.EmployeeID == emplID && x.Date > start && x.Date < end && x.LeaveLogStatus == status)
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LeaveLog>> GetLeaveLogResponsesByProjectIDAsync(int projectID, int? status)
        {
            try
            {
                if (status == null)
                {
                    return await this._dbContext.LeaveLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.Employee.EmployeeProjects.Any(p => p.ProjectID == projectID))
                                            .OrderByDescending(x => x.Date)
                                            .ToListAsync();
                }
                return await this._dbContext.LeaveLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.Employee.EmployeeProjects.Any(p => p.ProjectID == projectID) && x.LeaveLogStatus == status)
                                            .OrderByDescending(x => x.Date)
                                            .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LeaveLog>> GetLeaveLogResponsesByStatusAsync(int? status)
        {
            try
            {
                if (status == null)
                {
                    return await this._dbContext.LeaveLogs
                        .Include(x => x.Employee)
                        .OrderByDescending(x => x.Date)
                        .ToListAsync();
                }

                return await this._dbContext.LeaveLogs
                    .Include(x => x.Employee)
                    .Where(x => x.LeaveLogStatus == status)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateLeaveLogRequestAsync(LeaveLog leaveLog)
        {
            try
            {
                this._dbContext.Entry<LeaveLog>(leaveLog).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
