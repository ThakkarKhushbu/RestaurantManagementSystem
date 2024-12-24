using FluentAssertions;
using Moq;
using RestaurantManagementSystem.Infrastructure.Repositories.Interfaces;
using RestaurantManagementSystem.Infrastructure.Services;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;
using RestaurantManagementSystem.Models.Models;
using RestaurantManagementSystem.Test.Setup;

namespace RestaurantManagementSystem.Test.TestCase
{
    public class TableServiceTests : TestBase
    {
        private readonly ITableService _tableService;
        private readonly Mock<IRepository<Table>> _tableRepository;
        private readonly Mock<IRepository<Reservation>> _reservationRepository;
        private readonly Mock<ILogService> _log;

        public TableServiceTests()
        {
            _tableRepository = new Mock<IRepository<Table>>();
            _reservationRepository = new Mock<IRepository<Reservation>>();
            _log = new Mock<ILogService>();
            _tableService = new TableService(_tableRepository.Object, _reservationRepository.Object, _log.Object);
        }

        [Fact]
        public async Task GetAvailableTables_WithInvalidFilter_ShouldThrowException()
        {
            var filter = TestDataProvider.TableData.GetInvalidFilter();

            await _tableService.Invoking(s => s.GetTablesAsync(filter))
                .Should().ThrowAsync<Exception>()
                .WithMessage("ToTime must be greater than FromTime.");
        }

        [Fact]
        public async Task GetTableById_WithExistingId_ShouldReturnTable()
        {
            var existingTable = TestDataProvider.TableData.GetSampleTables().First();
            _tableRepository.Setup(r => r.GetByIdAsync(existingTable.Id))
                .ReturnsAsync(existingTable);

            var result = await _tableService.GetTableByIdAsync(existingTable.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(existingTable.Id);
        }

        [Fact]
        public async Task GetTableById_WithNonExistentId_ShouldThrowException()
        {
            var nonExistentId = Guid.Empty;

            _tableRepository.Setup(r => r.GetByIdAsync(nonExistentId))
                .ReturnsAsync((Table)null);

            await _tableService.Invoking(s => s.GetTableByIdAsync(nonExistentId))
                .Should().ThrowAsync<Exception>();
        }
    }
}
