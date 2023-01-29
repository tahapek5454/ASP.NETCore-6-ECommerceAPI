using E_CommerceAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        // IOC container'a eklemek üzere injectionlar tanımlanıyor

        public static void  AddPersistenceService(this IServiceCollection serviceCollection) {

            // Configuration sınıfını ben olusturdum app.jsondan veri alabilmek için
            serviceCollection.AddDbContext<ECommerceAPIDbContext>(options => 
                options.UseNpgsql(Configuration.ConnectionString));

        }
    }
}
