using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Service.Interface
{
    public interface IReservationService
    {
        Task<Reservation> Create(CreateReservationDto dto);
    }
}
