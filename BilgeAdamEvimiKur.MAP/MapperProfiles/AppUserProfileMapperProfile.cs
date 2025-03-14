using BilgeAdamEvimiKur.DTO.DTOs.ProfileDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class AppUserProfileMapperProfile : BaseMapperProfile
    {
        public AppUserProfileMapperProfile()
        {
            CreateMap<AppUserProfile, ProfileDTO>();
            CreateMap<ProfileDTO, AppUserProfile>()
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));

            CreateMap<AppUserProfileDTO, GetAppUserProfileResModel>().ReverseMap();
            CreateMap<AppUserProfileDTO, UpdateAppUserProfileReqModel>().ReverseMap();
            CreateMap<AppUserProfileDTO, UpdateAppUserProfileReqModel>().ReverseMap();
            CreateMap<ProfileDTO, AppUserProfileReqModel>().ReverseMap();
            CreateMap<AppUserProfileDTO, AppUserProfile>().ReverseMap();
            CreateMap<AppUserProfileDTO, AppUserProfileReqModel>().ReverseMap();
            CreateMap<AppUserProfileDTO, ProfileDTO>().ReverseMap();
        }
    }
}
