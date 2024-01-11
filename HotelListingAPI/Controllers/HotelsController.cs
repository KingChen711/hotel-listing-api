using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Dtos.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelRepository _hotelRepository;

        public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            var records = _mapper.Map<List<GetHotelDto>>(hotels);
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDetailDto>> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetDetailsById(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<GetHotelDetailDto>(hotel);

            return Ok(record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            var existHotel = await _hotelRepository.GetByIdAsync(id);
            if (existHotel == null)
            {
                return NotFound();
            }
            _mapper.Map(hotel, existHotel);

            try
            {
                await _hotelRepository.UpdateAsync(existHotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<GetHotelDto>> PostHotel(CreateHotelDto hotel)
        {
            var newHotel = _mapper.Map<Hotel>(hotel);
            var insertedHotel = await _hotelRepository.AddAsync(newHotel);
            return _mapper.Map<GetHotelDto>(insertedHotel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.Exists(id);
        }
    }
}
