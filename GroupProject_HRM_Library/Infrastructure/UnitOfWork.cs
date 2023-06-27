
using GroupProject_HRM_Library.DAO;
using GroupProject_HRM_Library.Models;

namespace GroupProject_HRM_Library.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private HumanResourceManagementContext _dbContext;
        private EmployeeDAO employeeDAO;
        private LeaveLogDAO leaveLogDAO;
        private IncomeDAO incomeDAO;
        private OvertimeLogDAO overtimeLogDAO;
        public UnitOfWork()
        {
            if (this._dbContext == null)
            {
                this._dbContext = DbFactory.Instance.InitDbContext();
            }
        }
        public OvertimeLogDAO OvertimeLogDAO
        {
            get
            {
                if (this.overtimeLogDAO == null)
                {
                    this.overtimeLogDAO = new OvertimeLogDAO(this._dbContext);
                }
                return this.overtimeLogDAO;
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

        public IncomeDAO IncomeDAO
        {
            get
            {
                if (this.incomeDAO == null)
                {
                    this.incomeDAO = new IncomeDAO(this._dbContext);
                }
                return this.incomeDAO;
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
