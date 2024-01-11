using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Dtos.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 6)]
        public required string Password { get; set; }
    }
}
