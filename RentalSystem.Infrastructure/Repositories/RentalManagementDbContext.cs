using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalSystem.Domain.Entities;
using RentalSystem.Domain.ValueObjects;

namespace RentalSystem.Infrastructure.Repositories
{
    public class RentalManagementDbContext : DbContext
    {
        public RentalManagementDbContext() { }

        public RentalManagementDbContext(DbContextOptions<RentalManagementDbContext> options)
            : base(options) { }

        public DbSet<EHouse> Houses => Set<EHouse>();
        public DbSet<EInvoiceUtility> InvoiceUtilities => Set<EInvoiceUtility>();
        public DbSet<ELandlord> Landlords => Set<ELandlord>();
        public DbSet<ENotification> Notifications => Set<ENotification>();
        public DbSet<EPaymentHistory> PaymentHistories => Set<EPaymentHistory>();
        public DbSet<ERentInvoice> RentInvoices => Set<ERentInvoice>();
        public DbSet<ERentalContract> RentalContracts => Set<ERentalContract>();
        public DbSet<ERoom> Rooms => Set<ERoom>();
        public DbSet<ETenant> Tenants => Set<ETenant>();
        public DbSet<ETenantDocument> TenantDocuments => Set<ETenantDocument>();
        public DbSet<EAddress> Addresses => Set<EAddress>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalManagementDbContext).Assembly);

            // 1. Detect Provider dynamically
            // Npgsql provider name is "Npgsql.EntityFrameworkCore.PostgreSQL"
            bool isPostgres = Database.IsNpgsql();

            // 2. Define Type mappings based on provider
            // Postgres uses 'numeric' and 'uuid'. SQL Server uses 'decimal' and 'uniqueidentifier'.
            string decimalType = isPostgres ? "numeric" : "decimal(18,2)";

            // Note: EF Core usually handles Guid/bool automatically if you don't 
            // force HasColumnType, but we'll stick to your decimal logic.

            var moneyConverter = new ValueConverter<Money, decimal>(
                m => m.Value,
                v => new Money(v));

            // 3. Apply configurations using the detected types
            ConfigureEntities(modelBuilder, moneyConverter, decimalType);

            // 4. Global Relationships
            ConfigureRelationships(modelBuilder);
        }

        private void ConfigureEntities(ModelBuilder modelBuilder, ValueConverter<Money, decimal> moneyConverter, string decimalType)
        {
            modelBuilder.Entity<ERoom>()
                .Property(r => r.MonthlyRent)
                .HasConversion(moneyConverter)
                .HasColumnType(decimalType);

            modelBuilder.Entity<ERentalContract>(entity =>
            {
                entity.Property(c => c.MonthlyRent).HasConversion(moneyConverter).HasColumnType(decimalType);
                entity.Property(c => c.DepositAmount).HasConversion(moneyConverter).HasColumnType(decimalType);
            });

            modelBuilder.Entity<ERentInvoice>(entity =>
            {
                entity.Property(i => i.RentAmount).HasConversion(moneyConverter).HasColumnType(decimalType);
                entity.Property(i => i.UtilityAmount).HasConversion(moneyConverter).HasColumnType(decimalType);
            });

            modelBuilder.Entity<EInvoiceUtility>(entity =>
            {
                entity.Property(u => u.Rate).HasConversion(moneyConverter).HasColumnType(decimalType);
                entity.Property(u => u.UnitOrQuantity).HasColumnType(decimalType);
            });

            modelBuilder.Entity<EPaymentHistory>()
                .Property(p => p.Amount)
                .HasConversion(moneyConverter)
                .HasColumnType(decimalType);
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EHouse>()
                .HasOne(h => h.Landlord)
                .WithMany(l => l.Houses)
                .HasForeignKey(h => h.LandlordId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EHouse>()
                .HasOne(h => h.Address)
                .WithMany(a => a.Houses)
                .HasForeignKey(h => h.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ELandlord>()
                .HasOne(l => l.Address)
                .WithMany(a => a.Landlords)
                .HasForeignKey(l => l.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
