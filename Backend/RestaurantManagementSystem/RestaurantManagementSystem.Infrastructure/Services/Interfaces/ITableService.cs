using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Filters;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Infrastructure.Services.Interfaces
{
    public interface ITableService
    {
        Task<Table> CreateTableAsync(CreateTableDto dto);

        Task<Table> GetTableByIdAsync(Guid id);

        Task<List<TableWithReservationsDto>> GetTablesAsync(Filters filter);
    }
}
