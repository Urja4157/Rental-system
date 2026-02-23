using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalSystem.Infrastructure.Repositories;

namespace RentalSystem.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. Read the switch from appsettings.json
            var provider = configuration["DatabaseProvider"];

            // 2. Identify this assembly so EF knows where the 'Migrations' folders are
            var assemblyName = typeof(RentalManagementDbContext).Assembly.GetName().Name;

            services.AddDbContext<RentalManagementDbContext>(options =>
            {
                if (provider == "Postgres")
                {
                    options.UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        // This allows EF to find Migrations/Postgres
                        x => x.MigrationsAssembly(assemblyName)
                    );
                }
                else if (provider == "SqlServer")
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("SqlServer"),
                        // This allows EF to find Migrations/SqlServer
                        x => x.MigrationsAssembly(assemblyName)
                    );
                }
                else
                {
                    throw new System.Exception("Unsupported or missing 'DatabaseProvider' in configuration.");
                }
            });

            return services;
        }
    }
}
