using HotelListingAPI.Data;

namespace HotelListingAPI.Contracts
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<Hotel?> GetDetailsById(int id);
    }
}
