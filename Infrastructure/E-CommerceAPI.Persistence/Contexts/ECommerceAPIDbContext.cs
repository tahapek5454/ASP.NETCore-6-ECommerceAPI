using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Domain.Entities.Common;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Contexts
{
    public class ECommerceAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        // DbContexti -> IdentityDbContexe çevirdik Aıthanticationlar için -> Appuser, AppRole ve key de = string olacak sekilde
        // Yapılan işlemi srviceRegistrationda da bildirmelisin!
        // IOC container uzerinde doldurucagiz options'ı
        public ECommerceAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        // Tablolarimizi eslestirdik class larimizla
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        // Burda kalıtımdan geliyor ya tek bir tabloda gerekli ayrımlar yapılarak olusucak(Table Per Hierarchy)
        public DbSet<OwnFile> OwnFiles { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }


        // Birebir iliski tanımlanacaksa bu islem uygulanmalidir
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasKey(o => o.Id);

            // indexledik ve uniqledik
            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(o => o.Id);

            builder.Entity<Order>()
                .HasOne(o => o.CompletedOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CompletedOrder>(c => c.OrderId);

            base.OnModelCreating(builder);
        }


        // bizim DbContex islemleri için olusacak interceptor -> model olusurke otomarik createDate' deger aticaz vb.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker : Entityler uzerinde yapilan degisikliklerin ya da yeni eklenen verinin yakalanmasini saglayan propertydir
            //Track edilen verileri yakalyip elde etmemizi sagliyor

            //ChangeTrackerdan BaseEntity tipindeki datalar yakaladım
            var datas = ChangeTracker.Entries<BaseEntity>();

            // her bir data icersinde gezerek datanın statini kontrol ettik ve ona gore saveden once araya girip update ya da create data'ya deger verdik
            // not: _ olması benim return den gelen degeri istemiyor oldugumu belirtmek bellekte yer harcanmiyor boylelikle
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreateDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate= DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
