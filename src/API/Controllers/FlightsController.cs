using Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IJourneyService _journeyService;

        public FlightsController(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJourney(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
                return BadRequest("Porfavor ingrese destino o origen validos. ");

            var dbJourney = await _journeyService.GetJourneyFromDbAsync(origin.ToUpper().Trim(), destination.ToUpper().Trim());

            if (dbJourney is not null)
            {
                return Ok(dbJourney);
            }

            var journey = await _journeyService.GetJourneyAsync(origin.ToUpper().Trim(), destination.ToUpper().Trim());

            return Ok(journey);
        }

    }
}
