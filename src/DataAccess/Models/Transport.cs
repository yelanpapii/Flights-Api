using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
