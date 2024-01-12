using HotelListingAPI.Contracts;
using HotelListingAPI.Dtos.Auth;
using HotelListingAPI.Dtos.Hotel;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger _logger;

        public AuthController(IAuthManager authManager, ILogger<AuthController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetHotelDto>> RegisterUser([FromBody] RegisterDto userDto)
        {
            _logger.LogInformation($"Registration Attemp for {userDto.Email}");
            var errors = await _authManager.Register(userDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponseDto>> LoginUser([FromBody] LoginDto userDto)
        {
            _logger.LogInformation($"Login Attemp for {userDto.Email}");
            var authResponse = await _authManager.Login(userDto);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }

        [HttpPost]
        [Route("refresh-token")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] AuthResponseDto body)
        {
            var authResponse = await _authManager.VerifyRefreshToken(body);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
