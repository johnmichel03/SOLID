using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SOLID.Principles.Config
{
    public class ConfigSettings : IConfigSettings
    {
        public ConfigSettings()
        {

        }
        // This is place to read from config source like web.config / database or appSettings.json file from .net core
        public string StoreType =>  "BACKUP";
    }
}
