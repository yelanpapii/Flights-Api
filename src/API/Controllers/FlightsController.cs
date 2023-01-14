using Business;
using Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
            var journey = await _journeyService.GetJourneyAsync(origin.ToUpper().Trim(), destination.ToUpper().Trim());

            return Ok(journey);
        }

    }
}
