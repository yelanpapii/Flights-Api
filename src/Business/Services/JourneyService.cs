using AutoMapper;
using Business.Repository.Interface;
using Business.Services.Interface;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IMapper _mapper;
        private readonly IFlightsService _flightsService;
        private readonly IJourneyRepository _journeyRepository;
        private readonly ILogger<JourneyService> _logger;

        public JourneyService(IFlightsService flightsService,
            IMapper mapper,
            IJourneyRepository journeyRepository,
            ILogger<JourneyService> logger)
        {
            _logger = logger;
            _journeyRepository = journeyRepository;
            _mapper = mapper;
            _flightsService = flightsService;
        }

        public async Task CreateJourneyAsync(JourneyDTO journey)
        {
            await _journeyRepository.CreateJourney(_mapper.Map<Journey>(journey));
        }

        public async Task<JourneyDTO> GetJourneyFromDbAsync(string origin, string destination)
        {
            var journey = await _journeyRepository.GetJourney(origin, destination);
            return _mapper.Map<JourneyDTO>(journey);
        }

        public async Task<JourneyDTO> GetJourneyAsync(string origin, string destination)
        {
            //Get flights by origin and destination
            var flight = await _flightsService.GetAllFlightAsync(origin, destination);

            if(flight is null)
            {
                _logger.LogError("No existen vuelos con las localidades ingresadas");
                return null;
            }

            var price = flight.Sum(x => x.Price);

            
            var flightMap = _mapper.Map<IEnumerable<Flight>>(flight);

            //Static Factory method to create a journey with mapped flights and price.
            var journey = Journey.Create(origin, destination, price, flightMap);

            //Add to db
            await _journeyRepository.CreateJourney(journey);
     
            return _mapper.Map<JourneyDTO>(journey);
        }
    }
}
