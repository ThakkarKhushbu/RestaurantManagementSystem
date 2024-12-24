using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new InvalidOperationException("The DbContext was not configured with a valid connection string.");
            }

            _ = optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Throw(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Guid table1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            Guid table2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            Guid table3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

            _ = modelBuilder.Entity<Table>().HasData(
                new Table
                {
                    Id = table1Id,
                    TableNumber = 1,
                    Location = "Main Hall",
                    SeatingCapacity = 4,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Table
                {
                    Id = table2Id,
                    TableNumber = 2,
                    Location = "Window Side",
                    SeatingCapacity = 6,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Table
                {
                    Id = table3Id,
                    TableNumber = 3,
                    Location = "Outdoor Patio",
                    SeatingCapacity = 8,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            _ = modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = Guid.Parse("A1111111-1111-1111-1111-111111111111"),
                    CustomerName = "John Doe",
                    ContactNumber = "+1234567890",
                    GuestCount = 4,
                    ReservationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    FromTime = new TimeOnly(18, 0),
                    ToTime = new TimeOnly(20, 0),
                    Status = ReservationStatus.Booked,
                    TableId = table1Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow                    
                },
                new Reservation
                {
                    Id = Guid.Parse("A2222222-2222-2222-2222-222222222222"),
                    CustomerName = "Jane Smith",
                    ContactNumber = "+1234567891",
                    GuestCount = 6,
                    ReservationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                    FromTime = new TimeOnly(19, 0),
                    ToTime = new TimeOnly(21, 0),
                    Status = ReservationStatus.Booked,
                    TableId = table2Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            _ = modelBuilder.Entity<Table>()
                .HasIndex(t => t.TableNumber)
                .IsUnique();

            _ = modelBuilder.Entity<Reservation>()
                .Property(r => r.ReservationDate)
                .HasConversion(
                    dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                    dateTime => DateOnly.FromDateTime(dateTime));

            _ = modelBuilder.Entity<Reservation>()
                .Property(r => r.FromTime)
                .HasConversion(
                    timeOnly => timeOnly.ToTimeSpan(),
                    timeSpan => TimeOnly.FromTimeSpan(timeSpan));

            _ = modelBuilder.Entity<Reservation>()
                .Property(r => r.ToTime)
                .HasConversion(
                    timeOnly => timeOnly.ToTimeSpan(),
                    timeSpan => TimeOnly.FromTimeSpan(timeSpan));

            _ = modelBuilder.Entity<Reservation>()
                .Property(r => r.CustomerName)
                .HasMaxLength(100)
                .IsRequired();

            _ = modelBuilder.Entity<Reservation>()
                .Property(r => r.ContactNumber)
                .HasMaxLength(20)
                .IsRequired();

            _ = modelBuilder.Entity<Table>()
                .Property(t => t.Location)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
