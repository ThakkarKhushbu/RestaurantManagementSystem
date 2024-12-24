using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Infrastructure.Repositories.Interfaces;
using RestaurantManagementSystem.Infrastructure.Validators.Interfaces;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Infrastructure.Validators
{
    public class BusinessRuleValidator(IRepository<Table> tableRepository,
                                       IRepository<Reservation> reservationRepository)
        : IBusinessRuleValidator
    {
        public async Task ValidateTableAvailabilityAsync(Guid tableId, DateOnly date, TimeOnly fromTime, TimeOnly toTime)
        {
            Table table = await tableRepository.GetByIdAsync(tableId) ?? throw new Exception($"Table {tableId} not found");
            
            if (!table.IsActive)
            {
                throw new Exception("Table is not active");
            }

            bool hasOverlap = await reservationRepository.GetQueryable()
                                                         .AnyAsync(r =>
                                                             r.Table!.Id == tableId &&
                                                             r.ReservationDate == date &&
                                                             r.Status != ReservationStatus.Cancelled &&
                                                             ((r.FromTime <= fromTime && r.ToTime > fromTime) ||
                                                              (r.FromTime < toTime && r.ToTime >= toTime) ||
                                                              (r.FromTime >= fromTime && r.ToTime <= toTime)));

            if (hasOverlap)
            {
                throw new Exception("Table is already reserved for this specified time");
            }
        }

        public async Task ValidateTableCapacityAsync(Guid tableId, int guestCount)
        {
            Table table = await tableRepository.GetByIdAsync(tableId) ?? throw new Exception($"Table {tableId} not found");
           
            if (guestCount > table.SeatingCapacity)
            {
                throw new Exception($"Guest count exceeds table capacity of {table.SeatingCapacity}");
            }
        }

        public async Task ValidateReservationCancellationAsync(Guid reservationId)
        {
            Reservation reservation = await reservationRepository.GetByIdAsync(reservationId) ?? throw new Exception($"Reservation {reservationId} not found");
           
            if (reservation.Status == ReservationStatus.Cancelled)
            {
                throw new Exception("Reservation is already cancelled");
            }

            var reservationDateTime = new DateTime(reservation.ReservationDate.Year, reservation.ReservationDate.Month, reservation.ReservationDate.Day, reservation.FromTime.Hour, reservation.FromTime.Minute, reservation.FromTime.Second);

            DateTime cancellationDeadline = reservationDateTime.AddMinutes(-30);

            if (DateTime.Now > cancellationDeadline)
            {
                throw new Exception("Reservations cannot be cancelled less than 30 minutes before the reserved time");
            }
        }
    }
}
