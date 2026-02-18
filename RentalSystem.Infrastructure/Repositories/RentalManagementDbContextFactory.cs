using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RentalSystem.Infrastructure.Repositories
{
    public class RentalDbContextFactory : IDesignTimeDbContextFactory<RentalManagementDbContext>
    {
        public RentalManagementDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RentalManagementDbContext>();

#if SQLSERVER
            optionsBuilder.UseSqlServer("Server=localhost;Database=RentalManagement;Trusted_Connection=True;TrustServerCertificate=True;",
                x => x.MigrationsAssembly("RentalSystem.Infrastructure"));
#elif POSTGRES
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RentalSystemDb;Port=5432; Username=postgres;Password=Nepal@123",
            x => x.MigrationsAssembly("RentalSystem.Infrastructure"));
#endif

            return new RentalManagementDbContext(optionsBuilder.Options);
        }
    }
}
