using HotelListingAPI.Dtos;

namespace HotelListingAPI.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<PagedResult<T>> GetAllAsync(QueryParameters queryParameters);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<bool> Exists(int id);
}