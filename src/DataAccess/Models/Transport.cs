using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models
{
    public partial class Transport
    {
        public Transport()
        {

        }

        public int Id { get; set; }
        public string FlightCarrier { get; set; }
        [Index("Ix_FlightNumber", Order = 1, IsUnique = true)]
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
