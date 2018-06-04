using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SOLID.Principles.Config
{
    public interface IConfigSettings
    {
        // It can be database name or connection string to connect the datasource
         string StoreType { get;}
    }
}
