using DataAccess.Models;
using System.Collections.Generic;

namespace Business
{
    public record JourneyDTO(
        string Origin,
        string Destination,
        double Price,
        List<Flight> Flights);

    public record FlightDTO(
        string Origin,
        string Destination,
        double Price,
        Transport Transport);

    public record ApiFlightResponseDTO(string DepartureStation,
        string ArrivalStation,
        string FlightCarrier,
        string FlightNumber,
        double Price);

    public record TransportDTO(
        string FlightCarrier,
        string FlightNumber);
}
