using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JM.SOLID.Principles.Config;
using JM.SOLID.Principles.Types;

namespace JM.SOLID.Principles.Data
{
    public class AccountDataStoreRepository : IAccountDataStoreRepository
    {
        // We can move the object creation logic to factory method of Unit Of Work Repository class.
        private readonly IConfigSettings _config;
        public AccountDataStoreRepository(IConfigSettings config)
        {
            _config = config;
        }

        public IAccountDataStore GetAccountDataSource()
        {
            if (_config.StoreType.ToUpper() == "BACKUP")
            {
                return new BackupAccountDataStore();
            }
            return new AccountDataStore();

        }
        
    }
}
