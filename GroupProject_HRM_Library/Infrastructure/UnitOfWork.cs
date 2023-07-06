
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
        private EmployeeProjectDAO employeeProjectDAO;
        private PayrollDAO payrollDAO;
        private BonusDAO bonusDAO;
        private TaxDAO taxDAO;
        private NotificationDAO notificationDAO;

        private ProjectDAO _projectDAO;
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
        public ProjectDAO ProjectDAO
        {
            get
            {
                if (_projectDAO == null)
                {
                    _projectDAO = new ProjectDAO(_dbContext);
                }
                return _projectDAO;
            }
        }
        public TaxDAO TaxDAO
        {
            get
            {
                if (taxDAO == null)
                {
                    taxDAO = new TaxDAO(_dbContext);
                }
                return taxDAO;
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

        public EmployeeProjectDAO EmployeeProjectDAO
        {
            get
            {
                if (this.employeeProjectDAO == null)
                {
                    this.employeeProjectDAO = new EmployeeProjectDAO(this._dbContext);
                }
                return this.employeeProjectDAO;
            }
        }

        public PayrollDAO PayrollDAO
        {
            get
            {
                if (this.payrollDAO == null)
                {
                    this.payrollDAO = new PayrollDAO(this._dbContext);
                }
                return this.payrollDAO;
            }
        }

        public BonusDAO BonusDAO
        {
            get
            {
                if (this.bonusDAO == null)
                {
                    this.bonusDAO = new BonusDAO(this._dbContext);
                }
                return this.bonusDAO;
            }
        }

        public NotificationDAO NotificationDAO
        {
            get
            {
                if (this.notificationDAO == null)
                {
                    this.notificationDAO = new NotificationDAO(this._dbContext);
                }
                return this.notificationDAO;
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
