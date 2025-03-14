using BilgeAdamEvimiKur.DTO.DTOs.SupplierDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.Supplier.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.Supplier.PureVMs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.MAP.MapperProfiles
{
    public class SupplierMapperProfile : BaseMapperProfile
    {
        public SupplierMapperProfile()
        {
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<SupplierDTO, Supplier>().ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)));

            CreateMap<SupplierDTO, SupplierResModel>().ReverseMap();
            CreateMap<SupplierDTO, CreateSupplierReqModel>().ReverseMap();
            CreateMap<SupplierDTO, UpdateSupplierReqModel>().ReverseMap();
        }

    }

}
