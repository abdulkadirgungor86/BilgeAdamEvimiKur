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
    public static class OrderDataSeed
    {
        public static void SeedOrders(ModelBuilder modelBuilder)
        {
            Order order1 = new Order()
            {
                ID = 1,
                ShippingAddress = new Lorem("en").Sentence(12),
                Email = "order1@outlook.com",
                NameDescripton = "Guest 1",
                Price = Math.Round(Convert.ToDecimal(new Commerce("en").Price()), 2)
            };
            modelBuilder.Entity<Order>().HasData(order1);

            Order order2 = new Order()
            {
                ID = 2,
                ShippingAddress = new Lorem("en").Sentence(12),
                AppUserID = 1,
                Price = Math.Round(Convert.ToDecimal(new Commerce("en").Price()), 2)
            };
            modelBuilder.Entity<Order>().HasData(order2);
         
        }
    }
}
