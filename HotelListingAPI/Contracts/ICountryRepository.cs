using HotelListingAPI.Data;

namespace HotelListingAPI.Contracts;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country?> GetDetailsById(int id);
}