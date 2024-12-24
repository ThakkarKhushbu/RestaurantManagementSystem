using Braintree;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Models;
using RestaurantManagementSystem.Repository.Interface;
using RestaurantManagementSystem.Service.Interface;

namespace RestaurantManagementSystem.Service.Service
{
    public class TableService(ITableRepository tableRepository) : ITableService
    {
        public async Task<PaginatedResult<Table>> GetAll(Filters filter)
        {
            return await tableRepository.GetAll(filter);
        }
    }
}
