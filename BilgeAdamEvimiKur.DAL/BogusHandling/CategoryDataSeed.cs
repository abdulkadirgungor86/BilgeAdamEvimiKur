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
    public static class CategoryDataSeed
    {
        public static void SeedCategories(ModelBuilder modelBuilder)
        {
            List<Category> categories = new List<Category>();
            for (int i = 1; i<=12; i++)
            {
                Category c = new Category()
                {
                    ID = i,
                    CategoryName = new Commerce("en").Categories(1)[0],
                    Description = new Lorem("en").Sentence(10)
                };

                categories.Add(c);
            }
            modelBuilder.Entity<Category>().HasData(categories);

        }
    }
}
