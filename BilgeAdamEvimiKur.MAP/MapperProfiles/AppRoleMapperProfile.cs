using BilgeAdamEvimiKur.DTO.DTOs.AppRoleDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class AppRoleMapperProfile : BaseMapperProfile
    {
        public AppRoleMapperProfile()
        {
            CreateMap<AppRole, AppRoleDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<AppRoleDTO, AppRole>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));

            CreateMap<AppRoleDTO, AppRoleResModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));
            
            CreateMap<AppRoleResModel, AppRoleDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<AppRoleDTO, CreateAppRoleReqModel>().ReverseMap();
            CreateMap<AppRoleDTO, UpdateAppRoleReqModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();


        }
    }
}
