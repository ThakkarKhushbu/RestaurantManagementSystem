using FluentAssertions;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Models;
using RestaurantManagementSystem.Repository.Interface;
using RestaurantManagementSystem.Repository.Repository;
using RestaurantManagementSystem.Test.Setup;

namespace RestaurantManagementSystem.Test.TestCase
{
    public class TableTest : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly ITableRepository _tableRepository;

        public TableTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _tableRepository = new TableRepository(_fixture.dBContext);
        }

        [Fact]
        public async Task GetAvailableTables_WithNoFilters_ReturnsAllActiveTables()
        {
            Braintree.PaginatedResult<Table> result = await _tableRepository.GetAll(new Filters());

            _ = result.Items.Should().HaveCount(2);
            _ = result.Items.Should().AllSatisfy(table => table.IsActive.Should().BeTrue());
        }

        [Fact]
        public async Task GetAvailableTables_WithCapacityFilter_ReturnsMatchingTables()
        {
            Filters filter = new() { MinSeatingCapacity = 6 };

            Braintree.PaginatedResult<Table> result = await _tableRepository.GetAll(filter);

            _ = result.Items.Should().HaveCount(1);
            _ = result.Items.Single().SeatingCapacity.Should().BeGreaterThanOrEqualTo(6);
        }

        [Fact]
        public async Task GetAvailableTables_WithTimeFilter_ExcludesReservedTables()
        {
            Reservation existingReservation = new()
            {
                CustomerName = "Existing Customer",
                ContactNumber = "1234567890",
                GuestCount = 4,
                ReservationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                FromTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(18)),
                ToTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(20)),
                Status = ReservationStatus.Booked,
                Table = _fixture.dBContext.Tables.First(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _ = _fixture.dBContext.Reservations.Add(existingReservation);
            _ = await _fixture.dBContext.SaveChangesAsync();

            Filters filter = new()
            {
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                FromTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(18)),
                ToTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(20))
            };

            Braintree.PaginatedResult<Table> result = await _tableRepository.GetAll(filter);

            _ = result.Items.Should().HaveCount(1);
            _ = result.Items.Should().NotContain(t => t.Id == existingReservation.Table.Id);
        }
    }
}
