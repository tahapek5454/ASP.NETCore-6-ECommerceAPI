using E_CommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
    {
        // packgae Manager Console' dısından migration olusturabilmek için gerekli ortam saglaniyor
        public ECommerceAPIDbContext CreateDbContext(string[] args)
        {
           
            DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString); // Configuration sınıfını ben olusturdum app.jsondan veri alabilmek için
            return new(dbContextOptionsBuilder.Options);
            
        }
    }
}
