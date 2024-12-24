using Braintree;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Service.Interface
{
    public interface ITableService
    {
        Task<PaginatedResult<Table>> GetAll(Filters filter);
    }
}
