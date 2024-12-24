using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models.Models
{
    public class Table
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public int TableNumber { get; set; }

        public required string Location { get; set; }

        public int SeatingCapacity { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
