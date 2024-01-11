using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.Dtos.Auth;
using HotelListingAPI.Dtos.Country;
using HotelListingAPI.Dtos.Hotel;

namespace HotelListingAPI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDetailDto>().ReverseMap();
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<Hotel, GetHotelDto>().ReverseMap();
            CreateMap<Hotel, GetHotelDetailDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();

            CreateMap<ApiUser, RegisterDto>().ReverseMap();
            CreateMap<ApiUser, LoginDto>().ReverseMap();
        }
    }
}
