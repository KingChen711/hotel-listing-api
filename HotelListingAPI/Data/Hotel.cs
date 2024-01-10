using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingAPI.Data
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public required string Name { get; set; }

        [Required]
        [StringLength(255)]
        public required string Address { get; set; }

        public double Rating { get; set; }

        // Foreign key for Country
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        // Navigation property
        public virtual Country? Country { get; set; }
    }
}
