using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Models.DTOs
{
    public class TableWithReservationsDto
    {
        public Guid Id { get; set; }

        public int TableNumber { get; set; }

        public required string Location { get; set; }

        public int SeatingCapacity { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }
}
