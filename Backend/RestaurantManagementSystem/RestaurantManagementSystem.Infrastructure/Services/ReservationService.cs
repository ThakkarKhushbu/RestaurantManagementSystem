using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Infrastructure.Repositories.Interfaces;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;
using RestaurantManagementSystem.Infrastructure.Validators.Interfaces;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Filters;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Infrastructure.Services
{
    public class ReservationService(IRepository<Reservation> reservationRepository,
                                    IBusinessRuleValidator businessValidator,
                                    ILogService log) : IReservationService
    {
        public async Task<Reservation> CreateReservationAsync(CreateReservationDto dto)
        {
            try
            {
                businessValidator.ValidateReservationTimeAsync(
                    dto.ReservationDate,
                    dto.FromTime,
                    dto.ToTime);

                await businessValidator.ValidateTableAvailabilityAsync(
                    dto.TableId,
                    dto.ReservationDate,
                    dto.FromTime,
                    dto.ToTime);

                await businessValidator.ValidateTableCapacityAsync(
                    dto.TableId,
                    dto.GuestCount);

                Reservation reservation = new()
                {
                    CustomerName = dto.CustomerName,
                    ContactNumber = dto.ContactNumber,
                    GuestCount = dto.GuestCount,
                    ReservationDate = dto.ReservationDate,
                    FromTime = dto.FromTime,
                    ToTime = dto.ToTime,
                    Status = ReservationStatus.Booked,
                    TableId = dto.TableId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _ = await reservationRepository.AddAsync(reservation);

                log.LogInformation($"Reservation {reservation.Id} created successfully");

                return reservation;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public async Task<List<Reservation>> GetReservationsAsync(Filters filter)
        {
            try
            {
                IQueryable<Reservation> query = reservationRepository.GetQueryable()
                    .Include(r => r.Table).AsQueryable();

                if (filter.Date.HasValue)
                {
                    query = query.Where(r => r.ReservationDate == filter.Date.Value);
                }

                if (filter.FromTime.HasValue)
                {
                    query = query.Where(r => r.FromTime >= filter.FromTime.Value);
                }

                if (filter.ToTime.HasValue)
                {
                    query = query.Where(r => r.ToTime <= filter.ToTime.Value);
                }

                if (filter.PageSize > 0)
                {
                    query = query.Skip((filter.PageNumber - 1) * filter.PageSize)
                               .Take(filter.PageSize);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public async Task<bool> CancelReservationAsync(Guid id)
        {
            try
            {
                await businessValidator.ValidateReservationCancellationAsync(id);

                Reservation? reservation = await reservationRepository.GetByIdAsync(id)
                    ?? throw new Exception($"No such reservation found for id {id}");

                reservation.Status = ReservationStatus.Cancelled;
                reservation.UpdatedAt = DateTime.UtcNow;

                await reservationRepository.UpdateAsync(reservation);

                log.LogInformation($"Reservation {id} cancelled successfully");

                return true;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public async Task<bool> IsTableAvailableAsync(
            Guid tableId,
            DateOnly date,
            TimeOnly fromTime,
            TimeOnly toTime)
        {
            try
            {
                bool hasOverlap = await reservationRepository.GetQueryable()
                    .AnyAsync(r =>
                        r.TableId == tableId &&
                        r.ReservationDate == date &&
                        r.Status != ReservationStatus.Cancelled &&
                        ((r.FromTime <= fromTime && r.ToTime > fromTime) ||
                         (r.FromTime < toTime && r.ToTime >= toTime) ||
                         (r.FromTime >= fromTime && r.ToTime <= toTime)));

                return !hasOverlap;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            try
            {
                return id == Guid.Empty ? throw new Exception("Valid ID is not provided!") : await reservationRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }
    }
}
