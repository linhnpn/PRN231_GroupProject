using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Infrastructure
{
    public class DbFactory
    {
        private HumanResourceManagementContext _dbContext;

        private DbFactory()
        {
        }
        private static DbFactory instance = null;
        private static readonly Object objectLock = new Object();
        public static DbFactory Instance
        {
            get
            {
                lock (objectLock)
                {
                    if (instance == null)
                    {
                        instance = new DbFactory();
                    }
                    return instance;
                }
            }
        }

        public HumanResourceManagementContext InitDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = new HumanResourceManagementContext();
            }
            return _dbContext;
        }
    }
}
