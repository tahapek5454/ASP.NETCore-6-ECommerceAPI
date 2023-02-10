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
using E_CommerceAPI.Application.Repositories.OwnFileRepository;
using E_CommerceAPI.Persistence.Repositories.OwnFileRepository;
using E_CommerceAPI.Application.Repositories.OwnFileRepository.ProductImageFileRepostitory;
using E_CommerceAPI.Persistence.Repositories.OwnFileRepository.ProductImageFileRepository;
using E_CommerceAPI.Application.Repositories.OwnFileRepository.InvoiceFileRepository;
using E_CommerceAPI.Persistence.Repositories.OwnFileRepository.InvoiceFileRepository;

namespace E_CommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        // IOC container'a eklemek üzere injectionlar tanımlanıyor

        public static void  AddPersistenceService(this IServiceCollection serviceCollection) {

            // Configuration sınıfını ben olusturdum app.jsondan veri alabilmek için
            serviceCollection.AddDbContext<ECommerceAPIDbContext>(options => 
                options.UseNpgsql(Configuration.ConnectionString));

            //AddDbContexin lifecyl'ı scope oldugundan scope ile devam edelim -> scope her requeste ozel injection yapar ve bitince dispose eder

            serviceCollection.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            serviceCollection.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            serviceCollection.AddScoped<IOrderReadRepository, OrderReadRepository>();
            serviceCollection.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            serviceCollection.AddScoped<IProductReadRepository, ProductReadRepository>();
            serviceCollection.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            serviceCollection.AddScoped<IOwnFileReadRepository, OwnFileReadRepository>();
            serviceCollection.AddScoped<IOwnFileWriteRepository, OwnFileWriteRepository>();

            serviceCollection.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            serviceCollection.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            serviceCollection.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            serviceCollection.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();



        }
    }
}
