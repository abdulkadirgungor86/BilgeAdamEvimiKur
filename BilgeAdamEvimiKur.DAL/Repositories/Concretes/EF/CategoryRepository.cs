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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyContext db) : base(db)
        {
        }
    }
}
