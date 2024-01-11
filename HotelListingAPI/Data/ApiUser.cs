using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Data
{
    public class ApiUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public required string LastName { get; set; }
    }
}
