using DataAccess.Persistence.Context;
using DataAccess.Persistence.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<BackendTestContext>(opts =>
            {
                opts.UseMySQL(
                    config.GetConnectionString("Backend"),
                    b => b.MigrationsAssembly(typeof(BackendTestContext).Assembly.FullName));
            });

            services.AddScoped<IAppContext>(x => x.GetService<BackendTestContext>());

            return services;
        }
    }
}
