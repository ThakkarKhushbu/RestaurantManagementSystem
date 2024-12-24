using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models.Models
{
    public class Table
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Range(1, 100)]
        public int TableNumber { get; set; }

        [Required]
        [StringLength(100)]
        public required string Location { get; set; }

        [Required]
        [Range(1, 20)]
        public int SeatingCapacity { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
