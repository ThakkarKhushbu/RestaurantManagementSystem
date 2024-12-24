using Braintree.Exceptions;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Models;
using RestaurantManagementSystem.Repository.Interface;

namespace RestaurantManagementSystem.Repository.Repository
{
    public class ReservationRepository(DBContext dBContext, ITableRepository tableRepository) : IReservationRepository
    {
        public async Task<Reservation> Create(CreateReservationDto dto)
        {
            try
            {
                Table? table = await tableRepository.GetById(dto.TableId) ?? throw new NotFoundException($"Table with ID {dto.TableId} not found or is inactive");
                
                if (table.SeatingCapacity < dto.GuestCount)
                {
                    throw new Exception("Selected table cannot accommodate the specified number of guests");
                }

                bool isTableAvailable = await CheckTableAvailability(dto.TableId, dto.ReservationDate, dto.FromTime, dto.ToTime);

                if (!isTableAvailable)
                {
                    throw new Exception("Table is not available for the selected time period");
                }

                Reservation reservation = new()
                {
                    CustomerName = dto.CustomerName,
                    ContactNumber = dto.ContactNumber,
                    GuestCount = dto.GuestCount,
                    ReservationDate = dto.ReservationDate,
                    FromTime = dto.FromTime,
                    ToTime = dto.ToTime,
                    Status = ReservationStatus.Booked,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Table = table
                };

                _ = dBContext.Reservations.Add(reservation);
                _ = await dBContext.SaveChangesAsync();

                return reservation;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while creating reservation", ex);
            }
        }

        private async Task<bool> CheckTableAvailability(Guid tableId, DateOnly reservationDate, TimeOnly fromTime, TimeOnly toTime)
        {
            return !await dBContext.Reservations
                    .AnyAsync(r => r.Table.Id == tableId &&
                              r.ReservationDate == reservationDate &&
                              r.Status != ReservationStatus.Cancelled &&
                              ((r.FromTime <= fromTime && r.ToTime > fromTime) ||
                               (r.FromTime < toTime && r.ToTime >= toTime) ||
                               (r.FromTime >= fromTime && r.ToTime <= toTime)));
        }
    }
}
