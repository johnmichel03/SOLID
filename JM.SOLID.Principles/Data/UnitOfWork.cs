using JM.SOLID.Principles.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SOLID.Principles.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfigSettings _config;
        public UnitOfWork(IConfigSettings config)
        {
            _config = config;
        }
        public IAccountDataStore AccountDataStore => new AccountDataStoreRepository(_config).GetAccountDataSource();

        public void Commit()
        {
           // Save unit of work here...
        }
    }
}
