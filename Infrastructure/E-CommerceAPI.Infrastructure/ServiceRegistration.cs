using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Abstractions.Tokens;
using E_CommerceAPI.Application.Services;
using E_CommerceAPI.Infrastructure.Enums;
using E_CommerceAPI.Infrastructure.Services;
using E_CommerceAPI.Infrastructure.Services.Storage;
using E_CommerceAPI.Infrastructure.Services.Storage.GCP;
using E_CommerceAPI.Infrastructure.Services.Storage.Local;
using E_CommerceAPI.Infrastructure.Services.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            //alternatif ilk göz ağrımız silmiyoruz :)
            serviceCollection.AddScoped<IFileServiceAlternative, FileServiceAlternative>();

            serviceCollection.AddScoped<IStorageService, StorageService>();

            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }

        // Bu yapacagimiz islem biziim storage'ımızn tam anlamıyla ne olacagını program.cs den girilicek
        // generic class ile belirlemek istememiz Local mi , Azure mu, AWS mi, GCP mi... Bunun gibi injection tanımla
        public static void AddStorage<T>(this IServiceCollection serviceCollection)
            where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }

        // -> Ustteki yapinin alternatifi
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    break;
                case StorageType.AWS:
                    break;
                case StorageType.GCP:
                    serviceCollection.AddScoped<IStorage, GCPStorage>();
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
