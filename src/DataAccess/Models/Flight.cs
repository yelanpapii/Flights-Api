using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace DataAccess.Models
{
    public partial class Flight
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public int TransportId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Journey> Journeys { get; set; }
        public virtual Transport Transport { get; set; }

        public Flight()
        {

        }
        public Flight(Transport transport, string origin, string destination, double price)
        {
            Transport = transport;
            Origin = origin;
            Destination = destination;
            Price = price;
        }

        //Static Factory Pattern.
        public static Flight Create(Transport transport, string Origin, string Destination, double Price)
        {
            return new Flight(transport, Origin, Destination, Price);
        }
    }
}
