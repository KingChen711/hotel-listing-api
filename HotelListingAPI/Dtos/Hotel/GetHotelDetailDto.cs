

using HotelListingAPI.Dtos.Country;

namespace HotelListingAPI.Dtos.Hotel
{
    public class GetHotelDetailDto : BaseHotelDto
    {
        public int Id { get; set; }
        public required GetCountryDto Country { get; set; }
    }
}
