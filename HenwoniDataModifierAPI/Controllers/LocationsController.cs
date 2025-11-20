using DotLiquid;
using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Pricing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HenwoniDataModifierAPI.Controllers
{
    [Route("api/locations")]
    [ApiController] 
    public class LocationsController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class GetLocationsResponse
        {
            public List<Continent> Continents { get; set; }
            public List<ContinentRegion> ContinentRegions { get; set; }
            public List<Country> Countries { get; set; }
            public List<State> States { get; set; }
            public List<City> Cities { get; set; }
            public List<Town> Towns { get; set; }
            public List<Language> Languages { get; set; }
            public List<Currency> Currencies { get; set; }
        }

        [HttpGet("get")]
        public async Task<ActionResult<GetLocationsResponse>> GetLocationsAsync()
        {
            GetLocationsResponse res = new GetLocationsResponse()
            {
                Continents = await _context.Continents.ToListAsync(),
                Countries = await _context.Countries.ToListAsync(),
                States = await _context.States.ToListAsync(),
                Cities = await _context.Cities.ToListAsync(),
                Towns = await _context.Towns.ToListAsync(),
                Currencies = await _context.Currencies.ToListAsync(),
                Languages = await _context.Languages.ToListAsync(),
            };
            return Ok(res);
        }
    }
}
