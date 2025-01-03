﻿using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Infrastructure.Repositories.Interfaces;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Filters;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.Infrastructure.Services
{
    public class TableService(IRepository<Table> tableRepository,
                              IRepository<Reservation> reservationRepository,
                              ILogService log) : ITableService
    {
        public async Task<Table> CreateTableAsync(CreateTableDto dto)
        {
            try
            {
                bool tableExists = await tableRepository.GetQueryable()
                    .AnyAsync(t => t.TableNumber == dto.TableNumber);

                if (tableExists)
                {
                    throw new Exception($"Table number {dto.TableNumber} already exists");
                }

                Table table = new()
                {
                    TableNumber = dto.TableNumber,
                    Location = dto.Location,
                    SeatingCapacity = dto.SeatingCapacity,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _ = await tableRepository.AddAsync(table);
                log.LogInformation($"Table {table.Id} created successfully");
                return table;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public async Task<Table> GetTableByIdAsync(Guid id)
        {
            try
            {
                return id == Guid.Empty ? throw new Exception("Valid ID is not provided!") : await tableRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public async Task<List<TableWithReservationsDto>> GetTablesAsync(Filters filter)
        {
            try
            {
                IQueryable<Table> tableQuery = tableRepository.GetQueryable();
                IQueryable<Reservation> reservationQuery = reservationRepository.GetQueryable();

                List<Reservation> reservedTables = [];

                if (filter.MinSeatingCapacity.HasValue)
                {
                    tableQuery = tableQuery.Where(t => t.SeatingCapacity >= filter.MinSeatingCapacity.Value);
                }

                if (filter.Date.HasValue && filter.FromTime.HasValue && filter.ToTime.HasValue)
                {
                    if (filter.ToTime <= filter.FromTime)
                    {
                        throw new Exception("ToTime must be greater than FromTime.");
                    }

                    reservedTables = reservationRepository.GetQueryable()
                        .Where(r =>
                            r.ReservationDate == filter.Date.Value &&
                            r.Status != ReservationStatus.Cancelled &&
                            ((r.FromTime <= filter.FromTime.Value && r.ToTime > filter.FromTime.Value) ||
                             (r.FromTime < filter.ToTime.Value && r.ToTime >= filter.ToTime.Value) ||
                             (r.FromTime >= filter.FromTime.Value && r.ToTime <= filter.ToTime.Value)))
                        .ToList();

                    tableQuery = tableQuery.Where(t => reservedTables.Select(x => x.TableId).Contains(t.Id));
                }
                else
                {
                    reservedTables = [.. reservationRepository.GetQueryable().Where(x=>
                    tableQuery.Any(y=>y.Id==x.TableId ))];
                }

                List<TableWithReservationsDto> tables = [.. tableQuery.Select(x => new TableWithReservationsDto()
                {
                    Id = x.Id,
                    Location = x.Location,
                    TableNumber = x.TableNumber,
                    SeatingCapacity = x.SeatingCapacity
                })];

                foreach (TableWithReservationsDto table in tables)
                {
                    table.Reservations = reservedTables.Where(x => x.TableId == table.Id).ToList();
                }

                return tables;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

    }
}
