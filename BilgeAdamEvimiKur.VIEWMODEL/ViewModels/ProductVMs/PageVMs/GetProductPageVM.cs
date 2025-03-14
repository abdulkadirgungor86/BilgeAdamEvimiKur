using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PageVMs
{
    public class GetProductPageVM
    {
        public IPagedList<ProductResModel> Products { get; set; }
    }
}
