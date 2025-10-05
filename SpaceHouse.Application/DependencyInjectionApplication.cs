using Microsoft.Extensions.DependencyInjection;
using SpaceHouse.Application.Interfaces;
using SpaceHouse.Application.Services;

namespace SpaceHouse.Application
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IHabitatService, HabitatService>();
            services.AddScoped<IZoneFonctionnelleService, ZoneFonctionnelleService>();
            services.AddScoped<ValidationService>();
            // Ajoutez ici les autres services d'application
            return services;
        }
    }
}
