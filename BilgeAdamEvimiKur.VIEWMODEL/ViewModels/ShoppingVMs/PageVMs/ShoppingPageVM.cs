using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ShoppingVMs.PageVMs
{
    public class ShoppingPageVM
    {
        public IPagedList<CategoryResModel> Categories { get; set; }
        public IPagedList<ProductResModel> Products { get; set; }
    }
}
