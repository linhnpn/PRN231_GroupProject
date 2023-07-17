using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DAO
{
    public class OvertimeLogDAO
    {
        private HumanResourceManagementContext _dbContext;
        public OvertimeLogDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateOvertimeLogAsync(OvertimeLog OvertimeLog)
        {
            try
            {
                await this._dbContext.OvertimeLogs.AddAsync(OvertimeLog);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OvertimeLog>> GetOvertimesAsync
                                            (int emplID, DateTime date)
        {
            try
            {
                return await this._dbContext.OvertimeLogs
                                            .Where(x => x.EmployeeID == emplID && x.OverTimeDate.Date == date.Date)
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OvertimeLog> GetOvertimeLogAsync(int id)
        {
            try
            {
                return await this._dbContext.OvertimeLogs
                                            .Include(x => x.Employee)
                                            .FirstOrDefaultAsync(x => x.OvertimeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OvertimeLog>> GetOvertimeLogResponsesByEmplIDAsync(int emplID, int? status)
        {
            try
            {
                if (status == null)
                {
                    return await this._dbContext.OvertimeLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.EmployeeID == emplID)
                                            .OrderByDescending(x => x.OverTimeDate)
                                            .ToListAsync();
                }
                return await this._dbContext.OvertimeLogs
                                            .Include(x => x.Employee)
                                            .OrderByDescending(x => x.OverTimeDate)
                                            .Where(x => x.EmployeeID == emplID && x.OvertimeLogStatus == status)
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OvertimeLog>> GetOvertimeLogResponsesByEmplIDAndDateAsync
                                            (int emplID, int? status, DateTime start, DateTime end)
        {
            try
            {
                if (status == null)
                {
                    return await this._dbContext.OvertimeLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.EmployeeID == emplID && x.OverTimeDate > start && x.OverTimeDate < end)
                                            .ToListAsync();
                }
                return await this._dbContext.OvertimeLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.EmployeeID == emplID && x.OverTimeDate > start && x.OverTimeDate < end && x.OvertimeLogStatus == status)
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OvertimeLog>> GetOvertimeLogResponsesByProjectIDAsync(int projectID, int? status)
        {
            try
            {
                if (status == null)
                {
                    return await this._dbContext.OvertimeLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.Employee.EmployeeProjects.Any(p => p.ProjectID == projectID))
                                            .OrderByDescending(x => x.OverTimeDate)
                                            .ToListAsync();
                }
                return await this._dbContext.OvertimeLogs
                                            .Include(x => x.Employee)
                                            .Where(x => x.Employee.EmployeeProjects.Any(p => p.ProjectID == projectID) && x.OvertimeLogStatus == status)
                                            .OrderByDescending(x => x.OverTimeDate)
                                            .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OvertimeLog>> GetOvertimeLogResponsesByStatusAsync(int? status)
        {
            try
            {
                if (status == null)
                {
                    return await this._dbContext.OvertimeLogs
                        .Include(x => x.Employee)
                        .OrderByDescending(x => x.OverTimeDate)
                        .ToListAsync();
                }

                return await this._dbContext.OvertimeLogs
                    .Include(x => x.Employee)
                    .OrderByDescending(x => x.OverTimeDate)
                    .Where(x => x.OvertimeLogStatus == status)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOvertimeLogRequestAsync(OvertimeLog OvertimeLog)
        {
            try
            {
                this._dbContext.Entry<OvertimeLog>(OvertimeLog).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
