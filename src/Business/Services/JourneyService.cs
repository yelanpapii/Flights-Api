using AutoMapper;
using Business.Repository.Interface;
using Business.Services.Interface;
using DataAccess.Models;
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

        public JourneyService(FlightsService flightsService,
            IMapper mapper,
            IJourneyRepository journeyRepository)
        {
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
            var flight = await _flightsService.GetAllFlightAsync(origin, destination);
            var price = flight.Sum(x => x.Price);

            var flightMap = _mapper.Map<IEnumerable<Flight>>(flight);

            var journey = Journey.Create(origin, destination, price, flightMap);

            await _journeyRepository.CreateJourney(journey);

            return _mapper.Map<JourneyDTO>(journey);
        }
    }
}
