using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppShop.Models;

namespace AppShop.Data
{
    public class AppShopContext : DbContext
    {
        public AppShopContext (DbContextOptions<AppShopContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}
