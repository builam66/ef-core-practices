using EFCP.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<AdventureWorks2022Context>(options =>
            {
                options.UseSqlServer(connectionString, x => x
                    .UseNetTopologySuite()
                    .UseHierarchyId());
            });

            services.AddScoped<IAdventureWorks2022Context, AdventureWorks2022Context>();

            return services;
        }
    }
}
