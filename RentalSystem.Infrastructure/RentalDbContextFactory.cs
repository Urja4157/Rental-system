using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RentalSystem.Infrastructure.Repositories;

namespace RentalSystem.Infrastructure
{
    public class RentalDbContextFactory : IDesignTimeDbContextFactory<RentalManagementDbContext>
    {
        //        public RentalManagementDbContext CreateDbContext(string[] args)
        //        {
        //            var optionsBuilder = new DbContextOptionsBuilder<RentalManagementDbContext>();

        //#if SQLSERVER
        //                    optionsBuilder.UseSqlServer("SqlServer",
        //                        x => x.MigrationsAssembly("RentalSystem.Infrastructure"));
        //#elif POSTGRES
        //            optionsBuilder.UseNpgsql("Postgres",
        //                            x => x.MigrationsAssembly("RentalSystem.Infrastructure"));
        //#endif

        //            return new RentalManagementDbContext(optionsBuilder.Options);
        //        }
        public RentalManagementDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // make sure it can find appsettings.json
                .AddJsonFile("appsettings.json")
                .Build();

            var provider = configuration["DatabaseProvider"];
            var optionsBuilder = new DbContextOptionsBuilder<RentalManagementDbContext>();

            if (provider == "SqlServer")
            {
                optionsBuilder.UseSqlServer(
                    configuration.GetConnectionString("SqlServer"),
                    x => x.MigrationsAssembly("RentalSystem.Infrastructure"));
            }
            else if (provider == "Postgres")
            {
                optionsBuilder.UseNpgsql(
                    configuration.GetConnectionString("Postgres"),
                    x => x.MigrationsAssembly("RentalSystem.Infrastructure"));
            }
            else
            {
                throw new Exception("Unknown database provider");
            }

            return new RentalManagementDbContext(optionsBuilder.Options);
        }
    }
}
