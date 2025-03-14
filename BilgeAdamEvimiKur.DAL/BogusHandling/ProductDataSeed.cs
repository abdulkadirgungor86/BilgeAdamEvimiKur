using BilgeAdamEvimiKur.COMMON.Tools.Services;
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
    public static class ProductDataSeed
    {
        private static readonly string ImagePathsFilePath = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/images/", "imagePathsForDataSeed.txt");

        public static void SeedProducts(ModelBuilder modelBuilder)
        {
            List<string> imagePaths;
            if (File.Exists(ImagePathsFilePath))
            {
                imagePaths = File.ReadAllLines(ImagePathsFilePath).ToList();
            }
            else
            {
                imagePaths = new List<string>();
                for (int i = 0; i < 12; i++)
                {
                    imagePaths.Add(ImageService.RandomSaveImagePNG());
                }
                File.WriteAllLines(ImagePathsFilePath, imagePaths);
            }

            List<Product> products = new List<Product>();
            for (int i = 1; i <= 12; i++)
            {
                Product product = new Product()
                {
                    ID = i,
                    ProductName = new Commerce("en").ProductName(),
                    UnitsInStock = i,
                    ImagePath = imagePaths[i - 1],
                    CategoryID = i,
                    SupplierID = (i % 2) + 1,
                    Price = Math.Round(Convert.ToDecimal(new Commerce("en").Price()), 2) // Virgulden sonra 2 basamak
                                                                                         // 1TL=100 kuruş
                };
                products.Add(product);
            }

            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
