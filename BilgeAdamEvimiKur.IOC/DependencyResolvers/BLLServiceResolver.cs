using BilgeAdamEvimiKur.BLL.Services.Abstracts;
using BilgeAdamEvimiKur.BLL.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.IOC.DependencyResolvers
{
    public static class BLLServiceResolver
    {
        public static void AddBLLCustomService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor(); 
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ISessionService, SessionService>();

        }
    }
}
