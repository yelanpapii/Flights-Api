using AutoMapper;
using Business.Common;
using Business.Services.Interface;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Business.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IMapper _mapper;
        private readonly IApiUrl _apiUrl;

        public FlightsService(IHttpClientFactory httpClient,
            IApiUrl apiUrl,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _apiUrl = apiUrl;
            _httpClient = httpClient;
        }

        //Get all flights by origin and destination.
        public async Task<IEnumerable<FlightDTO>> GetAllFlightAsync(string origin, string destination)
        {
            var httpClient = _httpClient.CreateClient();
            var response = await httpClient.GetAsync(_apiUrl.Url.AbsoluteUri);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var flightsResponse = await response.Content.ReadFromJsonAsync<List<ApiFlightResponseDTO>>();
            var list = new List<FlightDTO>();

            var directFlights = flightsResponse.Where(x => x.DepartureStation == origin && x.ArrivalStation == destination).FirstOrDefault();

            if (directFlights is not null)
            {
                list.Add(this.GetFlightsFromResponse(directFlights));

                return list;
            }

            var mainFlights = flightsResponse.Where(x => x.DepartureStation == origin)
                .SelectMany(f => flightsResponse.Where(f2 => f2.DepartureStation == f.ArrivalStation
                && f2.ArrivalStation == destination)
                .Select(f2 => new List<ApiFlightResponseDTO> { f, f2 }));

            if (directFlights is null && mainFlights is null)
                return null;

            list.AddRange(this.GetFlightsFromResponse(mainFlights));

            //parallel invoke to db
            return list;
        }

        private IEnumerable<FlightDTO> GetFlightsFromResponse(IEnumerable<List<ApiFlightResponseDTO>>? flights)
        {
            var list = new List<FlightDTO>();

            foreach (var flighsList in flights)
            {
                foreach (var item in flighsList)
                {
                    var transport = Transport.Create(item.FlightCarrier, item.FlightNumber);

                    var flight = Flight.Create(transport,
                        item.DepartureStation,
                        item.ArrivalStation,
                        item.Price);

                    list.Add(_mapper.Map<FlightDTO>(flight));
                }
            }

            return list;
        }

        private FlightDTO GetFlightsFromResponse(ApiFlightResponseDTO item)
        {
            var transport = Transport.Create(item.FlightCarrier, item.FlightNumber);

            var flight = Flight.Create(transport,
                item.DepartureStation,
                item.ArrivalStation,
                item.Price);

            return _mapper.Map<FlightDTO>(flight);
        }
    }
}