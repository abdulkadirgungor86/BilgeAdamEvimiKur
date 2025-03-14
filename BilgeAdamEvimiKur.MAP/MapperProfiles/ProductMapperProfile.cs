using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class ProductMapperProfile : BaseMapperProfile
    {
        public ProductMapperProfile()
        {
            CreateMap<ProductDTO, ProductResModel>().ReverseMap();
            CreateMap<ProductDTO, UpdateProductReqModel>().ReverseMap();
            CreateMap<ProductDTO, CreateProductReqModel>().ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Status != ENTITIES.Enums.DataStatus.Deleted ?  src.Category.CategoryName : null))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Status != ENTITIES.Enums.DataStatus.Deleted ?  src.Supplier.SupplierName : null));

            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));

        }

    }

}
