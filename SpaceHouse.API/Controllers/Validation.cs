using Microsoft.AspNetCore.Mvc;
using SpaceHouse.Application.DTOs;
using SpaceHouse.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaceHouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Validation : ControllerBase
    {

        private readonly ValidationService _validationService;

        public Validation(ValidationService validationService)
        {
            _validationService = validationService;
        }

        // POST api/<Validation>
        [HttpPost("habitat")]
        public async Task<ActionResult<List<ValidationResultDto>>> ValidateHabitat([FromBody] ValidateHabitatRequestDto request)
        {
            var results = await _validationService.ValidateHabitatAsync(request);
            return Ok(results);
        }


    }
}


