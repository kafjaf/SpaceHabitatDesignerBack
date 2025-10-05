using Microsoft.AspNetCore.Mvc;
using SpaceHouse.Application.DTOs;
using SpaceHouse.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaceHouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Habitats : ControllerBase
    {
        private readonly IHabitatService _habitatService;

        public Habitats(IHabitatService habitatService)
        {
            _habitatService = habitatService;
        }
        // GET: api/<Habitats>
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<HabitatTypeDto>>> GetHabitatTypes()
        {
            var types = await _habitatService.GetHabitatTypesAsync();
            return Ok(types);
        }
    }
}

