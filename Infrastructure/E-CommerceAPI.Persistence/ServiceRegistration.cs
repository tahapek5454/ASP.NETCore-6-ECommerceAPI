using E_CommerceAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceAPI.Application.Repositories.CustomerRepository;
using E_CommerceAPI.Persistence.Repositories.CustomerRepository;
using E_CommerceAPI.Application.Repositories.OrderRepository;
using E_CommerceAPI.Persistence.Repositories.OrderRepository;
using E_CommerceAPI.Application.Repositories.ProductRepository;
using E_CommerceAPI.Persistence.Repositories.ProductRepository;

namespace E_CommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        // IOC container'a eklemek üzere injectionlar tanımlanıyor

        public static void  AddPersistenceService(this IServiceCollection serviceCollection) {

            // Configuration sınıfını ben olusturdum app.jsondan veri alabilmek için
            serviceCollection.AddDbContext<ECommerceAPIDbContext>(options => 
                options.UseNpgsql(Configuration.ConnectionString), ServiceLifetime.Singleton);

            serviceCollection.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
            serviceCollection.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();

            serviceCollection.AddSingleton<IOrderReadRepository, OrderReadRepository>();
            serviceCollection.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();

            serviceCollection.AddSingleton<IProductReadRepository, ProductReadRepository>();
            serviceCollection.AddSingleton<IProductWriteRepository, ProductWriteRepository>();

        }
    }
}
