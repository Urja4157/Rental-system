using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentalSystem.Infrastructure.Repositories;

namespace RentalSystem.Infrastructure
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using RentalManagementDbContext context =
                scope.ServiceProvider.GetRequiredService<RentalManagementDbContext>();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<RentalManagementDbContext>>();

            try
            {
                logger.LogInformation("Applying migrations for {Provider}...", context.Database.ProviderName);
                context.Database.Migrate();
                logger.LogInformation("Migrations applied successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while applying migrations.");
                // In development, you want to know if this fails immediately
                throw;
            }
        }
    }
}