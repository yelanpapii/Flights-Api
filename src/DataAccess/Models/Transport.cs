namespace DataAccess.Models
{
    public sealed class Transport
    {
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }

        public Transport(string flightCarrier, string flightNumber)
        {
            FlightCarrier = flightCarrier;
            FlightNumber = flightNumber;
        }

        //Static Factory Pattern.
        public static Transport Create(string FlightCarrier, string FlightNumber)
        {
            return new Transport(FlightCarrier, FlightNumber);
        }
    }
}
