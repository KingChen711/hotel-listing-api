using HotelListingAPI.Dtos.Hotel;

namespace HotelListingAPI.Dtos.Country
{
    public class GetCountryDetailDto : BaseCountryDto
    {
        public int Id { get; set; }
        public List<GetHotelDto> Hotels { get; set; } = [];

    }
}
