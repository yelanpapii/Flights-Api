namespace DataAccess.Models
{
    public class Transport
    {
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }

        public Transport(string flightCarrier, string flightNumber)
        {
            FlightCarrier = flightCarrier;
            FlightNumber = flightNumber;
        }

        public static Transport Create(string FlightCarrier, string FlightNumber)
        {
            return new Transport(FlightCarrier, FlightNumber);
        }
    }
}
