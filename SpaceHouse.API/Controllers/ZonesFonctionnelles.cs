using Microsoft.AspNetCore.Mvc;
using SpaceHouse.Application.DTOs;
using SpaceHouse.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaceHouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonesFonctionnelles : ControllerBase
    {
        private readonly IZoneFonctionnelleService _zoneFonctionnelleService;

        public ZonesFonctionnelles(IZoneFonctionnelleService zoneFonctionnelleService)
        {
            _zoneFonctionnelleService = zoneFonctionnelleService;
        }
        // GET: api/<ZonesFonctionnelles>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZoneDeFonctionnementDefautDto>>> GetDefaultZones()
        {
            var zones = await _zoneFonctionnelleService.GetAllDefaultZonesAsync();
            return Ok(zones);
        }
    }
}


