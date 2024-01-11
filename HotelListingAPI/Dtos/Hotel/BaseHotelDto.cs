using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Dtos.Hotel
{
    public abstract class BaseHotelDto
    {
        [Required]
        [StringLength(255)]
        public required string Name { get; set; }

        [Required]
        [StringLength(255)]
        public required string Address { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public double Rating { get; set; }

        public int CountryId { get; set; }
    }
}
