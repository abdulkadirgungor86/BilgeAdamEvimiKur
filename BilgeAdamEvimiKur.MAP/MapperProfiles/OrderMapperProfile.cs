using BilgeAdamEvimiKur.DTO.DTOs.OrderDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PageVMs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class OrderMapperProfile : BaseMapperProfile
    {
        public OrderMapperProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.NameDescription, opt => opt.MapFrom(src => src.NameDescripton));

            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.NameDescripton, opt => opt.MapFrom(src => src.NameDescription))
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));

            CreateMap<OrderReqModel, OrderResponseModelDTO>().ReverseMap();
            CreateMap<OrderPageVM, OrderRequestPageVMDTO>().ReverseMap();
            CreateMap<OrderResponseModelDTO, OrderDTO>().ReverseMap();

            CreateMap<OrderDTO, OrderResModel>()
                .ForMember(dest => dest.NameDescription, opt => opt.MapFrom(src => src.NameDescription))
                .ReverseMap();
                
        }
    }
}
