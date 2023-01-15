using Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IJourneyService _journeyService;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(IJourneyService journeyService,
            ILogger<FlightsController> logger)
        {
            _logger = logger;
            _journeyService = journeyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJourney(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                _logger.LogError("Origin o Destination invalida");
                return BadRequest("Porfavor ingrese destino o origen validos.");
            }
                

            var dbJourney = await _journeyService.GetJourneyFromDbAsync(origin.ToUpper().Trim(), destination.ToUpper().Trim());

            if (dbJourney is not null)
            {
                return Ok(dbJourney);
            }

            //Si journey no existe en la base de datos, solicitud a la api de newshore.
            var journey = await _journeyService.GetJourneyAsync(origin.ToUpper().Trim(), destination.ToUpper().Trim());

            if(journey is null)
            {
                _logger.LogError("No existe journey con las localidades ingresadas");
                return BadRequest("No existe journey con las localidades ingresadas");
            }

            return Ok(journey);
        }

    }
}
