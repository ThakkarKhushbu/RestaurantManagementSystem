using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Test.Setup
{
    public class DatabaseFixture : IDisposable
    {
        private readonly DbContextOptions<DBContext> options;
        public DBContext dBContext { get; }

        public DatabaseFixture()
        {
            options = new DbContextOptionsBuilder<DBContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;

            dBContext = new DBContext(options);
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            List<Table> tables =
            [
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                TableNumber = 1,
                Location = "Main Hall",
                SeatingCapacity = 4,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                TableNumber = 2,
                Location = "Window Side",
                SeatingCapacity = 6,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        ];

            dBContext.Tables.AddRange(tables);
            _ = dBContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
