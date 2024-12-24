using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Filters;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Infrastructure.Services.Interfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(CreateReservationDto dto);

        Task<Reservation> GetReservationByIdAsync(Guid id);

        Task<List<Reservation>> GetReservationsAsync(Filters filter);

        Task<bool> CancelReservationAsync(Guid id);

        Task<bool> IsTableAvailableAsync(Guid tableId, DateOnly date, TimeOnly fromTime, TimeOnly toTime);
    }
}
