using BilgeAdamEvimiKur.COMMON.Tools.Models;
using BilgeAdamEvimiKur.DTO.DTOs.PaymentDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ShoppingDTOs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.PaymentVMs.PureVMs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ShoppingVMs.PageVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class ShoppingMapperProfile : BaseMapperProfile
    {
        public ShoppingMapperProfile()
        {
            CreateMap<CartItem, ProductDTO>().ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice)).ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<CartDTO, CartPageVM>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
            CreateMap<PaymentReqModel, PaymentRequestModelDTO>().ReverseMap();
        }

    }
}
