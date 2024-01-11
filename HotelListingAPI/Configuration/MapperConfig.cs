using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.Dtos.Country;
using HotelListingAPI.Dtos.Hotel;

namespace HotelListingAPI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDetailDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<Hotel, GetHotelDto>().ReverseMap();
        }
    }
}
