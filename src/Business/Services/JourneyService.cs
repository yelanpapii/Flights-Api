using AutoMapper;
using Business.Services.Interface;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IMapper _mapper;
        private readonly IFlightsService _flightsService;

        public JourneyService(FlightsService flightsService,
            IMapper mapper)
        {
            _mapper = mapper;
            _flightsService = flightsService;
        }

        public async Task<JourneyDTO> GetJourneyAsync(string origin, string destination)
        {
            var flight = await _flightsService.GetAllFlightAsync(origin, destination);

            var price = flight.Sum(x => x.Price);

            var flightMap = _mapper.Map<IEnumerable<Flight>>(flight);

            var journey = Journey.Create(origin, destination, price, flightMap);

            return _mapper.Map<JourneyDTO>(journey);
        }
    }
}
