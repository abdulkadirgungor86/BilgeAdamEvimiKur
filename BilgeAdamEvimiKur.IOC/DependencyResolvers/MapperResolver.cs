using AutoMapper;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.MAP.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.IOC.DependencyResolvers
{
    public static class MapperResolver
    {
        public static void AddMapperServices(this IServiceCollection services)
        {
            MapperConfiguration mapConfiguration = new MapperConfiguration(x =>
            {
                x.AddProfile(new AppRoleMapperProfile());
                x.AddProfile(new AppUserProfileMapperProfile());
                x.AddProfile(new AppUserMapperProfile());
                x.AddProfile(new CategoryMapperProfile());
                x.AddProfile(new OrderDetailMapperProfile());
                x.AddProfile(new OrderMapperProfile());
                x.AddProfile(new ProductMapperProfile());
                x.AddProfile(new ShoppingMapperProfile());
                x.AddProfile(new SupplierMapperProfile());

            });

            IMapper mapper = mapConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
