using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.IOC.DependencyResolvers
{
    public static class ApplicationCookieResolver
    {
        public static void AddApplicationCookieService(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/user/SignIn";
                options.AccessDeniedPath = "/user/SignIn";
            });
        }
    }
}
