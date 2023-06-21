using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Infrastructure
{
    public interface IUnitOfWork
    {
        public void Commit();
        public Task CommitAsync();
    }
}
