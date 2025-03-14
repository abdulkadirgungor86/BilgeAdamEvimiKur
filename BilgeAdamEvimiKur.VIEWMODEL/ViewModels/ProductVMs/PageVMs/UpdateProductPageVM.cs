﻿using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.Supplier.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PageVMs
{
    public class UpdateProductPageVM
    {
      public List<SupplierResModel>? Suppliers { get; set; }
      public List<CategoryResModel>? Categories { get; set; }
      public UpdateProductReqModel? Product { get; set; }
    }
}
