namespace RestaurantManagementSystem.Models.DTOs
{
    public class CreateReservationDto
    {
        public required string CustomerName { get; set; }

        public required string ContactNumber { get; set; }

        public int GuestCount { get; set; }

        public DateOnly ReservationDate { get; set; }

        public TimeOnly FromTime { get; set; }

        public TimeOnly ToTime { get; set; }

        public Guid TableId { get; set; }
    }
}
