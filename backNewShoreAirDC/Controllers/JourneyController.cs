using BusinessLayer.BusinessDisponibility;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backNewShoreAirDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly ILogger<JourneyController> _logger;
        private readonly IDisponibilityBusiness _disponibilityBusiness;

        public JourneyController(ILogger<JourneyController> logger, IDisponibilityBusiness disponibilityBusiness)
        {
            _logger = logger;
            _disponibilityBusiness = disponibilityBusiness;
        }
        // GET: api/<JourneyController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{origin}/{destination}")]
        public ActionResult Get(string origin, string destination)
        {
            var flight = _disponibilityBusiness.GetDisponibility(new Request() { Origin = origin, Destination = destination });
            return Ok(flight);
        }
    }
}
