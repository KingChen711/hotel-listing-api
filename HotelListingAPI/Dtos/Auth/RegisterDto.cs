using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        [StringLength(255)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 6)]
        public required string Password { get; set; }
    }
}
