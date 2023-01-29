using E_CommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Contexts
{
    public class ECommerceAPIDbContext : DbContext
    {
        // IOC container uzerinde doldurucagiz options'ı
        public ECommerceAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        // Tablolarimizi eslestirdik class larimizla
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }    
    }
}
