using LibraryLab.Databases;
using LibraryLab.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace LibraryLab.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddLibraryLabServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContenxt>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static IServiceCollection RegisterEndpointsFromAssemblyContaining<T>(this IServiceCollection services)
        {
            var assembly = typeof(T).Assembly;

            var endpointTypes = assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpoint)) && t is { IsClass: true, IsAbstract: false, IsInterface: false });

            var serviceDescriptors = endpointTypes
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);
            return services;
        }

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

            foreach (var endpoint in endpoints)
            {
                endpoint.MapEndpoint(app);
            }

            return app;
        }
    }
}
