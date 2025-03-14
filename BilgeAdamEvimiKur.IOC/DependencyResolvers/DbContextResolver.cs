using BilgeAdamEvimiKur.DAL.ContextClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.IOC.DependencyResolvers
{
    public static class DbContextResolver
    {
        public static void AddDbContextService(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();

            IConfiguration? configuration = provider.GetService<IConfiguration>();

            services.AddDbContextPool<MyContext>( (optBuilder) => 
            {
                optBuilder.UseSqlServer(configuration.GetConnectionString   ("ProjeConnection")).UseLazyLoadingProxies();

                optBuilder.ConfigureWarnings(warnings => 
                       warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            });
        }
    }
}
