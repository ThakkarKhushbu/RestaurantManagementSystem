using FluentAssertions;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Models;
using RestaurantManagementSystem.Repository.Interface;
using RestaurantManagementSystem.Repository.Repository;
using RestaurantManagementSystem.Test.Setup;

namespace RestaurantManagementSystem.Test.TestCase
{
    public class ReservationTest : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly ITableRepository _tableRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _tableRepository = new TableRepository(_fixture.dBContext);
            _reservationRepository = new ReservationRepository(_fixture.dBContext, _tableRepository);
        }

        [Fact]
        public async Task CreateReservation_WithValidData_CreatesReservation()
        {
            Models.DTOs.CreateReservationDto dto = TestData.ValidReservationDto;

            Reservation result = await _reservationRepository.Create(dto);

            _ = result.Should().NotBeNull();
            _ = result.CustomerName.Should().Be(dto.CustomerName);
            _ = result.Status.Should().Be(ReservationStatus.Booked);
        }

        [Fact]
        public async Task CreateReservation_WithInvalidTableId_ThrowsException()
        {
            Models.DTOs.CreateReservationDto dto = TestData.ValidReservationDto;
            dto.TableId = Guid.NewGuid();

            _ = await _reservationRepository.Invoking(s => s.Create(dto))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task CreateReservation_WithExistingReservation_ThrowsException()
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

            Models.DTOs.CreateReservationDto dto = TestData.ValidReservationDto;
            dto.TableId = existingReservation.Table.Id;
            dto.ReservationDate = existingReservation.ReservationDate;
            dto.FromTime = existingReservation.FromTime;
            dto.ToTime = existingReservation.ToTime;

            _ = await _reservationRepository.Invoking(s => s.Create(dto))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task CreateReservation_WithExceededCapacity_ThrowsException()
        {
            Models.DTOs.CreateReservationDto dto = TestData.ValidReservationDto;
            dto.GuestCount = 10;

            _ = await _reservationRepository.Invoking(s => s.Create(dto))
                .Should().ThrowAsync<Exception>();
        }
    }
}
