using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class AppUserMapperProfile : BaseMapperProfile
    {
        public AppUserMapperProfile()
        {
            CreateMap<AppUser, AppUserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<AppUserDTO, AppUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));

            CreateMap<AppUser, UserRegisterRequestModelDTO>().ReverseMap();
            CreateMap<UserRegisterReqModel, UserRegisterRequestModelDTO>().ReverseMap();
            CreateMap<UserSignInReqModel, UserSignInRequestModelDTO>().ReverseMap();

            CreateMap<AppUserDTO, UpdateUserResModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<AppUserDTO, GetUserResModel>().ReverseMap();
            CreateMap<AppUserDTO, UpdateUserResModel>().ReverseMap();
            CreateMap<AppUserDTO, UpdateUserModel>().ReverseMap();
            CreateMap<SendPassToEmailDTO, SendPassToEmailReqModel>().ReverseMap();
            CreateMap<ChangePasswordDTO, ChangePasswordReqModel>().ReverseMap();
        }
    }

}
