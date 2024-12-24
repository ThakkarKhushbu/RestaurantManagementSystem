using Braintree;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Repository.Interface
{
    public interface ITableRepository
    {
        Task<Table?> GetById(Guid Id);

        Task<PaginatedResult<Table>> GetAll(Filters filter);
    }
}
