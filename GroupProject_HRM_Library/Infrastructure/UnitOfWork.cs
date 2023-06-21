﻿using GroupProject_HRM_Library.DAO;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private MyStoreContext _dbContext;
        private ProductDAO productDAO;
        public UnitOfWork()
        {
            if (this._dbContext == null)
            {
                this._dbContext = DbFactory.Instance.InitDbContext();
            }
        }

        public ProductDAO ProductDAO
        {
            get
            {
                if (this.productDAO == null)
                {
                    this.productDAO = new ProductDAO(this._dbContext);
                }
                return this.productDAO;
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
