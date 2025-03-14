using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.IOC.DependencyResolvers
{
    public static class SessionResolver
    {
        public static void AddSessionService(this IServiceCollection services)
        {
            services.AddSession(x =>
            {
                x.IdleTimeout = TimeSpan.FromDays(1);
                x.Cookie.HttpOnly = true;
                x.Cookie.IsEssential = true;
                
            });
        }
    }
}
