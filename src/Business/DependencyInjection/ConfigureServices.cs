using Business.Common;
using Business.Repository;
using Business.Repository.Interface;
using Business.Services;
using Business.Services.Interface;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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

            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IJourneyRepository, JourneyRepository>();

            services.AddScoped<IFlightsService, FlightsService>();

            services.AddScoped<IJourneyService, JourneyService>();

            return services;
        }
    }
}
