using BilgeAdamEvimiKur.ENTITIES.Models;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DAL.BogusHandling
{
    public static class SupplierDataSeed
    {
        public static void SeedSuppliers(ModelBuilder modelBuilder)
        {
            Supplier supplier1 = new Supplier()
            {
                ID = 1,
                SupplierName = "Test1 A.Ş.",
                ContactName = "Test1",
                Email = "Test1@gmail.com",
                Phone = "532-555-3322",
                WebSite = "www.test1.com",
                Address = new Lorem("en").Sentence(10),
                City = new Lorem("en").Sentence(1),
                Country = new Lorem("en").Sentence(1)
            };
            modelBuilder.Entity<Supplier>().HasData(supplier1);

            Supplier supplier2 = new Supplier()
            {
                ID = 2,
                SupplierName = "Test2 A.Ş.",
                ContactName = "Test2",
                Email = "Test2@gmail.com",
                Phone = "550-555-4477",
                WebSite = "www.test2.com",
                Address = new Lorem("en").Sentence(10),
                City = new Lorem("en").Sentence(1),
                Country = new Lorem("en").Sentence(1)
            };
            modelBuilder.Entity<Supplier>().HasData(supplier2);
        }

    }
}
