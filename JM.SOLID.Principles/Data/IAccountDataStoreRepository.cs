using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SOLID.Principles.Data
{
    public interface IAccountDataStoreRepository
    {
        IAccountDataStore GetAccountDataSource();
    }
}
