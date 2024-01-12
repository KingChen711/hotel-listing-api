using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Dtos.Country;
using HotelListingAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountriesController(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countryRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDetailDto>> GetCountry(int id)
        {
            var country = await _countryRepository.GetDetailsById(id);

            if (country == null)
            {
                throw new NotFoundException("Not found country");
            }

            var record = _mapper.Map<GetCountryDetailDto>(country);

            return Ok(record);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto country)
        {
            if (id != country.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            var existCountry = await _countryRepository.GetByIdAsync(id);
            if (existCountry == null)
            {
                return NotFound();
            }

            _mapper.Map(country, existCountry);

            try
            {
                await _countryRepository.UpdateAsync(existCountry);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GetCountryDto>> PostCountry(CreateCountryDto country)
        {
            var newCountry = _mapper.Map<Country>(country);
            var insertedCountry = await _countryRepository.AddAsync(newCountry);
            return _mapper.Map<GetCountryDto>(insertedCountry);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countryRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countryRepository.Exists(id);
        }
    }
}
