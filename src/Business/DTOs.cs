using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public record JourneyDTO(
        string Origin,
        string Destination,
        double Price,
        List<Flight> Flights);

    public record FlightDTO(Transport Transport,
        string Origin,
        string Destination,
        double Price);
    
    public record ApiFlightResponseDTO(string DepartureStation,
        string ArrivalStation,
        string FlightCarrier,
        string FlightNumber,
        double Price);
    
    public record TransportDTO(
        string FlightCarrier,
        string FlightNumber);
}
