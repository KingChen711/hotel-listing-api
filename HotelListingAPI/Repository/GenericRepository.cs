using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelListingDbContext _context;
        public GenericRepository(HotelListingDbContext context)
        {
            _context = context;
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<PagedResult<T>> GetAllAsync(QueryParameters queryParameters)
        {
            var totalCount = await _context.Set<T>().CountAsync();
            var items = await _context.Set<T>().Skip(queryParameters.PageSize * (queryParameters.PageNumber - 1))
                .Take(queryParameters.PageSize).ToListAsync();
            return new PagedResult<T>(
                )
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }
    }
}
