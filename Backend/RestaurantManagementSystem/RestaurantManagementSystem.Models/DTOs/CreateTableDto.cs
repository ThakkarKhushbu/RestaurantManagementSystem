namespace RestaurantManagementSystem.Models.DTOs
{
    public class CreateTableDto
    {
        public int TableNumber { get; set; }

        public required string Location { get; set; }

        public int SeatingCapacity { get; set; }
    }
}
