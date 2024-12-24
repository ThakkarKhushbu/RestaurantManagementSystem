using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Infrastructure.Repositories.Interfaces;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Infrastructure.Repositories
{
    public class Repository<T>(DBContext context, ILogService log) : IRepository<T> where T : class
    {
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                return await context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                _ = await context.Set<T>().AddAsync(entity);
                _ = await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                _ = await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            try
            {
                _ = context.Set<T>().Remove(entity);
                _ = await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return context.Set<T>().AsQueryable();
        }
    }
}
