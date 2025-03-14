using BilgeAdamEvimiKur.CONF.Configurations;
using BilgeAdamEvimiKur.DAL.BogusHandling;
using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BilgeAdamEvimiKur.DAL.ContextClasses
{
    public class MyContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppUserProfileConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new SupplierConfiguration());

            UserRoleDataSeed.SeedUsers(builder);
            CategoryDataSeed.SeedCategories(builder);
            SupplierDataSeed.SeedSuppliers(builder);
            ProductDataSeed.SeedProducts(builder);
            OrderDataSeed.SeedOrders(builder);
            OrderDetailDataSeed.SeedOrderDetails(builder);
            ProfileDataSeed.SeedProfiles(builder);
            
        }

        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserProfile> Profiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


    }

}
