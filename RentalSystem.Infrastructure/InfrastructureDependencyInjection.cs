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
            var provider = configuration["DatabaseProvider"];

            services.AddDbContext<RentalManagementDbContext>(options =>
            {
                if (provider == "Postgres")
                {
                    options.UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        // Point EF to the Postgres project for migrations
                        x => x.MigrationsAssembly("RentalSystem.Migrations.Postgres")
                    );
                }
                else if (provider == "SqlServer")
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("SqlServer"),
                        // Point EF to the SQL Server project for migrations
                        x => x.MigrationsAssembly("RentalSystem.Migrations.SqlServer")
                    );
                }
            });

            return services;
        }
    }
}
