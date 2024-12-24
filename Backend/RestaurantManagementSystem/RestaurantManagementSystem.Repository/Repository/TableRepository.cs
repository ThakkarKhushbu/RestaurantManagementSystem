using Braintree;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Enums;
using RestaurantManagementSystem.Models.Models;
using RestaurantManagementSystem.Repository.Interface;

namespace RestaurantManagementSystem.Repository.Repository
{
    public class TableRepository(DBContext dBContext) : ITableRepository
    {

        public async Task<Table?> GetById(Guid Id)
        {
            return await dBContext.Tables.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<PaginatedResult<Table>> GetAll(Filters filter)
        {
            try
            {
                IQueryable<Table> query = dBContext.Tables
                    .Where(t => t.IsActive);

                if (filter.MinSeatingCapacity.HasValue)
                {
                    query = query.Where(t => t.SeatingCapacity >= filter.MinSeatingCapacity.Value);
                }

                if (filter.Date.HasValue && filter.FromTime.HasValue && filter.ToTime.HasValue)
                {
                    List<Guid> reservedTableIds = await dBContext.Reservations
                        .Where(r =>
                            r.ReservationDate == filter.Date.Value &&
                            r.Status != ReservationStatus.Cancelled &&
                            ((r.FromTime <= filter.FromTime.Value && r.ToTime > filter.FromTime.Value) ||
                             (r.FromTime < filter.ToTime.Value && r.ToTime >= filter.ToTime.Value) ||
                             (r.FromTime >= filter.FromTime.Value && r.ToTime <= filter.ToTime.Value)))
                        .Select(r => r.Table.Id)
                        .ToListAsync();

                    query = query.Where(t => !reservedTableIds.Contains(t.Id));
                }

                int totalCount = await query.CountAsync();

                List<Table> tables = await query
                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                  .Take(filter.PageSize)
                                  .ToListAsync();

                return new PaginatedResult<Table>(totalCount, filter.PageSize, tables);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while retrieving available tables", ex);
            }
        }
    }
}
