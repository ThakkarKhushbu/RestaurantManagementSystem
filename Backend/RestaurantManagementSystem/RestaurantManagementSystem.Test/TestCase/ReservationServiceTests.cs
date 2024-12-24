using FluentAssertions;
using Moq;
using RestaurantManagementSystem.Infrastructure.Repositories.Interfaces;
using RestaurantManagementSystem.Infrastructure.Services;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;
using RestaurantManagementSystem.Infrastructure.Validators.Interfaces;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Models;
using RestaurantManagementSystem.Test.Setup;

namespace RestaurantManagementSystem.Test.TestCase
{
    public class ReservationServiceTests : TestBase
    {
        private readonly ITableService _tableService;
        private readonly IReservationService _reservationService;
        private readonly Mock<IRepository<Table>> _tableRepository;
        private readonly Mock<IRepository<Reservation>> _reservationRepository;
        private readonly Mock<ILogService> _log;
        private readonly Mock<IBusinessRuleValidator> _businessValidator;

        public ReservationServiceTests()
        {
            _tableRepository = new Mock<IRepository<Table>>();
            _reservationRepository = new Mock<IRepository<Reservation>>();

            _log = new Mock<ILogService>();

            _tableService = new TableService(_tableRepository.Object, _reservationRepository.Object, _log.Object);

            _businessValidator = new Mock<IBusinessRuleValidator>();

            _reservationService = new ReservationService(
                _reservationRepository.Object,
                _businessValidator.Object,
                _log.Object);
        }

        [Fact]
        public async Task CreateReservation_WithValidData_ShouldCreateReservation()
        {
            Models.DTOs.CreateReservationDto newReservation = TestDataProvider.ReservationData.GetNewReservation();
            Table table = TestDataProvider.TableData.GetSampleTables()
                .First(t => t.Id == newReservation.TableId);


            _ = _reservationRepository.Setup(r => r.AddAsync(It.IsAny<Reservation>()))
                .ReturnsAsync((Reservation r) => r);

            Reservation result = await _reservationService.CreateReservationAsync(newReservation);

            _ = result.Should().NotBeNull();
            _ = result.Status.Should().Be(ReservationStatus.Booked);
            _ = result.CustomerName.Should().Be(newReservation.CustomerName);
        }

        [Fact]
        public async Task CancelReservation_WithValidReservation_ShouldCancelReservation()
        {
            Reservation existingReservation = TestDataProvider.ReservationData.GetSampleReservations().First();
            _ = _reservationRepository.Setup(r => r.GetByIdAsync(existingReservation.Id))
                .ReturnsAsync(existingReservation);

            _ = await _reservationService.CancelReservationAsync(existingReservation.Id);

            _reservationRepository.Verify(r => r.UpdateAsync(It.Is<Reservation>(
                r => r.Id == existingReservation.Id && r.Status == ReservationStatus.Cancelled
            )), Times.Once);
        }

        [Fact]
        public async Task CancelReservation_WithNonExistentReservation_ShouldThrowException()
        {
            Guid nonExistentId = TestDataProvider.ReservationData.NonExistentReservationId;
            _ = _reservationRepository.Setup(r => r.GetByIdAsync(nonExistentId))
                .ReturnsAsync((Reservation)null);

            _ = await _reservationService.Invoking(s => s.CancelReservationAsync(nonExistentId))
                .Should().ThrowAsync<Exception>();
        }
    }
}
