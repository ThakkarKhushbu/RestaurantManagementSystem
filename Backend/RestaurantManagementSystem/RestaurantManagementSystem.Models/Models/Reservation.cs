using RestaurantManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public required string CustomerName { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public required string ContactNumber { get; set; }

        [Required]
        [Range(1, 20)]
        public int GuestCount { get; set; }

        [Required]
        public DateOnly ReservationDate { get; set; }

        [Required]
        public TimeOnly FromTime { get; set; }

        [Required]
        public TimeOnly ToTime { get; set; }

        [Required]
        public ReservationStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public Guid? TableId { get; set; }

        [ForeignKey("TableId")]
        public Table? Table { get; set; }
    }
}
