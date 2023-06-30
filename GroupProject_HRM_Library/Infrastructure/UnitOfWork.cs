
using GroupProject_HRM_Library.DAO;
using GroupProject_HRM_Library.Models;

namespace GroupProject_HRM_Library.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private HumanResourceManagementContext _dbContext;
        //private ProductDAO productDAO;
        private TaxDAO _taxDAO;
        private ProjectDAO _projectDAO;
        public UnitOfWork()
        {
            if (this._dbContext == null)
            {
                this._dbContext = DbFactory.Instance.InitDbContext();
            }
        }

        //public ProductDAO ProductDAO
        //{
        //    get
        //    {
        //        if (this.productDAO == null)
        //        {
        //            this.productDAO = new ProductDAO(this._dbContext);
        //        }
        //        return this.productDAO;
        //    }
        //}
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
                if(_taxDAO == null)
                {
                    _taxDAO = new TaxDAO(_dbContext);
                }
                return _taxDAO;
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
