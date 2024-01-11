using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Dtos.Country
{
    public abstract class BaseCountryDto
    {
        [Required]
        [StringLength(255)]
        public required string Name { get; set; }

        [Required]
        [StringLength(255)]
        public required string ShortName { get; set; }
    }
}
