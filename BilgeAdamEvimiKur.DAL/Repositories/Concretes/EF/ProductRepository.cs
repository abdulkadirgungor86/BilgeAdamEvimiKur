using BilgeAdamEvimiKur.DAL.ContextClasses;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DAL.Repositories.Concretes.EF
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MyContext db) : base(db)
        {
        }
    }
}
