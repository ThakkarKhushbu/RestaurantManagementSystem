namespace RestaurantManagementSystem.Models.Filters
{
    public class Filters
    {
        public DateOnly? Date { get; set; }

        public TimeOnly? FromTime { get; set; }

        public TimeOnly? ToTime { get; set; }

        public int? MinSeatingCapacity { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
