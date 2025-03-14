using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.BLL.Managers.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.IOC.DependencyResolvers
{
    public static class ManagerResolver
    {
        public static void AddManagerServices(this IServiceCollection services)
        {


            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IOrderDetailManager, OrderDetailManager>();
            services.AddScoped<ISupplierManager, SupplierManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IAppRoleManager, AppRoleManager>();
            services.AddScoped<IProfileManager, ProfileManager>();


        }

    }
}
