using BilgeAdamEvimiKur.DTO.DTOs.CategoryDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class CategoryMapperProfile : BaseMapperProfile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CategoryDTO, CreateCategoryReqModel>().ReverseMap();
            CreateMap<CategoryDTO, UpdateCategoryReqModel>().ReverseMap();
            CreateMap<CategoryDTO, CategoryResModel>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap().ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));

            CreateMap<CategoryResModel, CategoryResponseModelDTO>().ReverseMap();
        }
    }

}
