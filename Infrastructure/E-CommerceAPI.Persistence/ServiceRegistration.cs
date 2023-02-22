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
using E_CommerceAPI.Domain.Entities.Identity;
using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Persistence.Services;
using E_CommerceAPI.Application.Abstractions.Services.Authentications;
using E_CommerceAPI.Application.Repositories.BasketRepository;
using E_CommerceAPI.Persistence.Repositories.BasketRepository;
using E_CommerceAPI.Application.Repositories.BasketItemRepository;
using E_CommerceAPI.Persistence.Repositories.BasketItemRepository;

namespace E_CommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        // IOC container'a eklemek üzere injectionlar tanımlanıyor

        public static void  AddPersistenceService(this IServiceCollection serviceCollection) {

            // Configuration sınıfını ben olusturdum app.jsondan veri alabilmek için
            serviceCollection.AddDbContext<ECommerceAPIDbContext>(options => 
                options.UseNpgsql(Configuration.ConnectionString));
            // IdentityDbContext kullanacagimizdan onu bildirmeliyiz entityFrameworke de ne ile kullancagi bildirmeliyiz
            serviceCollection.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<ECommerceAPIDbContext>();

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

            serviceCollection.AddScoped<IUserService, UserService>();

            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IInternalAuthenticationService, AuthService>();
            serviceCollection.AddScoped<IExternalAuthenticationService, AuthService>();

            serviceCollection.AddScoped<IBasketReadRepository, BasketReadRepository>();
            serviceCollection.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

            serviceCollection.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            serviceCollection.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

            serviceCollection.AddScoped<IBasketService, BasketService>();




        }
    }
}
