using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
