using BilgeAdamEvimiKur.DTO.DTOs.OrderDetailDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ShoppingDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderDetailVMs.PureVms.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class OrderDetailMapperProfile : BaseMapperProfile
    {
        public OrderDetailMapperProfile()
        {

            CreateMap<OrderDetail, OrderDetailDTO>()
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.Order.ShippingAddress))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<OrderDetailDTO, OrderDetail>()
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));

            CreateMap<OrderDetailDTO, CartItemDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();

            CreateMap<OrderDetailDTO, OrderDetailResModel>()
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)))
                .ReverseMap();
        }
    }
}
