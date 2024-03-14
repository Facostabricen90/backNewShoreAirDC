using DomainLayer.Models.ApiData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicesLayer;

namespace backNewShoreAirDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsDataController : ControllerBase
    {
        private readonly ApiNewShoreContext _context;

        public FlightsDataController(ApiNewShoreContext context )
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<DataApiFlights>> Get()
        {
            return await _context.DataApiFlights.ToListAsync();
        }
    }
}
