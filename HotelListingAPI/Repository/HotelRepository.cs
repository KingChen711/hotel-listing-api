using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private readonly HotelListingDbContext _context;
        public HotelRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Hotel?> GetDetailsById(int id)
        {
            return await _context.Hotels.Include(q => q.Country).FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
