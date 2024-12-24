using RestaurantManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string CustomerName { get; set; }

        public required string ContactNumber { get; set; }

        public int GuestCount { get; set; }

        public DateOnly ReservationDate { get; set; }

        public TimeOnly FromTime { get; set; }

        public TimeOnly ToTime { get; set; }

        public ReservationStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [ForeignKey("TableId")]
        public required Table Table { get; set; }
    }
}
