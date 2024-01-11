namespace HotelListingAPI.Dtos.Hotel
{
    public class GetHotelDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }
    }
}
