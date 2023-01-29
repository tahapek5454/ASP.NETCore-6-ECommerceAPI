using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence
{
    internal static class Configuration
    {
        public static string ConnectionString
        {
            get
            {

                // app.setting'deki json datadan connection stringi cekcek
                ConfigurationManager configurationManager = new ConfigurationManager();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/E-CommerceAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("PostgreSQL");

            }
        }
    }
}
