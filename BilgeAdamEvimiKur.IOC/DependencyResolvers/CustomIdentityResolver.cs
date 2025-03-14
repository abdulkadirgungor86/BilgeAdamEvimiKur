using BilgeAdamEvimiKur.DAL.ContextClasses;
using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.IOC.DependencyResolvers
{
    public static class CustomIdentityResolver
    {
        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole<int>>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequiredLength = 4;
                x.SignIn.RequireConfirmedEmail = true;
                x.Password.RequireNonAlphanumeric = false;

                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                x.Lockout.MaxFailedAccessAttempts = 5;
                x.Lockout.AllowedForNewUsers = false;
                x.SignIn.RequireConfirmedAccount = false;

            }).AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

        }
    }
}
