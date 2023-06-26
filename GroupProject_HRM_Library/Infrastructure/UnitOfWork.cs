
using GroupProject_HRM_Library.DAO;
using GroupProject_HRM_Library.Models;

namespace GroupProject_HRM_Library.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private HumanResourceManagementContext _dbContext;
        private EmployeeDAO employeeDAO;
        private LeaveLogDAO leaveLogDAO;
        public UnitOfWork()
        {
            if (this._dbContext == null)
            {
                this._dbContext = DbFactory.Instance.InitDbContext();
            }
        }

        public EmployeeDAO EmployeeDAO
        {
            get
            {
                if (this.employeeDAO == null)
                {
                    this.employeeDAO = new EmployeeDAO(this._dbContext);
                }
                return this.employeeDAO;
            }
        }

        public LeaveLogDAO LeaveLogDAO
        {
            get
            {
                if (this.leaveLogDAO == null)
                {
                    this.leaveLogDAO = new LeaveLogDAO(this._dbContext);
                }
                return this.leaveLogDAO;
            }
        }

        public void Commit()
        {
            this._dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
