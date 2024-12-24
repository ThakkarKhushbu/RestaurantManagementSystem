using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Test.Setup
{
    public abstract class TestBase : IDisposable
    {
        public readonly DBContext _context;

        protected TestBase()
        {
            DbContextOptions<DBContext> options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DBContext(options);
        }

        public void Dispose()
        {
            _ = _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
