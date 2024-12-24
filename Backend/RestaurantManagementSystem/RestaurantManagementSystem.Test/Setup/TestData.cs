using RestaurantManagementSystem.Models.DTOs;

namespace RestaurantManagementSystem.Test.Setup
{
    public static class TestData
    {
        public static CreateReservationDto ValidReservationDto => new()
        {
            CustomerName = "John Doe",
            ContactNumber = "+1234567890",
            GuestCount = 4,
            ReservationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            FromTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(18)),
            ToTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(20)),
            TableId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };
    }
}
