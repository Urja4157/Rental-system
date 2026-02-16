using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalSystem.Domain.Entities;
using RentalSystem.Domain.ValueObjects;

namespace RentalSystem.Infrastructure.Repositories
{
    public class RentalManagementDbContext : DbContext
    {
        public RentalManagementDbContext()
        {

        }
        public RentalManagementDbContext(DbContextOptions<RentalManagementDbContext> options)
       : base(options)
        {
        }

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
            // Apply all configuration classes automatically
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalManagementDbContext).Assembly);
            // -------------------- MONEY VALUE OBJECT CONVERSIONS --------------------
            var moneyConverter = new ValueConverter<Money, decimal>(
                m => m.Value,
                v => new Money(v));

            modelBuilder.Entity<ERoom>()
                .Property(r => r.MonthlyRent)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ERentalContract>()
                .Property(c => c.MonthlyRent)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ERentalContract>()
                .Property(c => c.DepositAmount)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ERentInvoice>()
                .Property(i => i.RentAmount)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ERentInvoice>()
                .Property(i => i.UtilityAmount)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<EInvoiceUtility>()
                .Property(u => u.Rate)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<EInvoiceUtility>()
                .Property(u => u.UnitOrQuantity)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<EPaymentHistory>()
                .Property(p => p.Amount)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
            // UTC DateTime enforcement + SQL Server datetime2 column type
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            //        v => v,
            //        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //    var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            //        v => v,
            //        v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

            //    foreach (var property in entityType.GetProperties())
            //    {
            //        if (property.ClrType == typeof(DateTime))
            //        {
            //            property.SetColumnType("datetime2");
            //            property.SetValueConverter(dateTimeConverter);
            //        }
            //        else if (property.ClrType == typeof(DateTime?))
            //        {
            //            property.SetColumnType("datetime2");
            //            property.SetValueConverter(nullableDateTimeConverter);
            //        }
            //    }
            //}
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EHouse>()
       .HasOne(h => h.Landlord)
       .WithMany(l => l.Houses)
       .HasForeignKey(h => h.LandlordId)
       .OnDelete(DeleteBehavior.Restrict); // <-- prevent cascade

            modelBuilder.Entity<EHouse>()
                .HasOne(h => h.Address)
                .WithMany(a => a.Houses)
                .HasForeignKey(h => h.AddressId)
                .OnDelete(DeleteBehavior.Restrict); // <-- prevent cascade

            modelBuilder.Entity<ELandlord>()
                .HasOne(l => l.Address)
                .WithMany(a => a.Landlords)
                .HasForeignKey(l => l.AddressId)
                .OnDelete(DeleteBehavior.Restrict); // <-- prevent cascade
        }
    }
}
