using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Filters;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Test.Setup
{
    public static class TestDataProvider
    {
        public static class TableData
        {
            public static readonly Guid TableId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            public static readonly Guid TableId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");

            public static List<Table> GetSampleTables()
            {
                return new List<Table>
            {
                new()
                {
                    Id = TableId1,
                    TableNumber = 1,
                    Location = "Main Hall",
                    SeatingCapacity = 4,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = TableId2,
                    TableNumber = 2,
                    Location = "Window Side",
                    SeatingCapacity = 6,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            }

            public static Filters GetFilterWithAllParameters()
            {
                return new Filters
                {
                    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    FromTime = new TimeOnly(18, 0),
                    ToTime = new TimeOnly(20, 0),
                    MinSeatingCapacity = 4,
                    PageNumber = 1,
                    PageSize = 10
                };
            }

            public static Filters GetFilterWithOnlyCapacity()
            {
                return new Filters
                {
                    MinSeatingCapacity = 6,
                    PageNumber = 1,
                    PageSize = 10
                };
            }

            public static Filters GetInvalidFilter()
            {
                return new Filters
                {
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    FromTime = new TimeOnly(14, 0),
                    ToTime = new TimeOnly(12, 0), // Invalid: ToTime < FromTime
                    PageNumber = 1,
                    PageSize = 10
                };
            }
        }

        public static class ReservationData
        {
            public static readonly Guid ReservationId1 = Guid.Parse("A1111111-1111-1111-1111-111111111111");
            public static readonly Guid ReservationId2 = Guid.Parse("A2222222-2222-2222-2222-222222222222");
            public static readonly Guid NonExistentReservationId = Guid.Parse("B9999999-9999-9999-9999-999999999999");

            public static List<Reservation> GetSampleReservations()
            {
                return new List<Reservation>
            {
                new()
                {
                    Id = ReservationId1,
                    CustomerName = "John Doe",
                    ContactNumber = "+1234567890",
                    GuestCount = 4,
                    ReservationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    FromTime = new TimeOnly(18, 0),
                    ToTime = new TimeOnly(20, 0),
                    Status = ReservationStatus.Booked,
                    TableId = TableData.TableId1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = ReservationId2,
                    CustomerName = "Jane Smith",
                    ContactNumber = "+1234567891",
                    GuestCount = 6,
                    ReservationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    FromTime = new TimeOnly(19, 0),
                    ToTime = new TimeOnly(21, 0),
                    Status = ReservationStatus.Booked,
                    TableId = TableData.TableId2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            }

            public static CreateReservationDto GetNewReservation()
            {
                return new CreateReservationDto
                {
                    CustomerName = "New Customer",
                    ContactNumber = "+1234567892",
                    GuestCount = 4,
                    ReservationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                    FromTime = new TimeOnly(18, 0),
                    ToTime = new TimeOnly(20, 0),
                    TableId = TableData.TableId1
                };
            }
        }
    }
}
