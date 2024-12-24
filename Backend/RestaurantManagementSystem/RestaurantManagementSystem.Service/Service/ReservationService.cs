using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Models;
using RestaurantManagementSystem.Repository.Interface;
using RestaurantManagementSystem.Service.Interface;

namespace RestaurantManagementSystem.Service.Service
{
    public class ReservationService(IReservationRepository reservationRepository) : IReservationService
    {
        public async Task<Reservation> Create(CreateReservationDto dto)
        {
            return await reservationRepository.Create(dto);
        }
    }
}
