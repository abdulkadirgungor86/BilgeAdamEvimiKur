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
    public static class OrderDetailDataSeed
    {
        public static void SeedOrderDetails(ModelBuilder modelBuilder)
        {
            OrderDetail orderDetail1 = new OrderDetail()
            {
                OrderID = 1,
                ProductID = 1,
                Price = Math.Round(Convert.ToDecimal(new Commerce("en").Price()), 2),
                Quantity = 3
            };
            modelBuilder.Entity<OrderDetail>().HasData(orderDetail1);

            OrderDetail orderDetail2 = new OrderDetail()
            {
                OrderID = 2,
                ProductID = 2,
                Price = Math.Round(Convert.ToDecimal(new Commerce("en").Price()), 2),
                Quantity = 5
            };
            modelBuilder.Entity<OrderDetail>().HasData(orderDetail2);
        }
    }
}
