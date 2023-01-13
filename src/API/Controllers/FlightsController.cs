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
        public async Task<JourneyDTO> GetJourney(string origin, string destination)
        {
            return await _journeyService.GetJourneyAsync(origin, destination);
        }

    }
}
