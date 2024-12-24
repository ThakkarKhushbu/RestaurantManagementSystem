using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Repository.Interface
{
    public interface IReservationRepository
    {
        Task<Reservation> Create(CreateReservationDto dto);
    }
}
