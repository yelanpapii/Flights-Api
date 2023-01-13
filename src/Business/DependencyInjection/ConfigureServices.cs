using Business.Common;
using Business.Services;
using Business.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHttpClient();

            services.AddScoped<IApiUrl, ApiUrl>();

            services.AddScoped<FlightsService>();

            services.AddScoped<IFlightsService, FlightsService>();

            services.AddScoped<IJourneyService, JourneyService>();
            
            return services;
        }
    }
}
