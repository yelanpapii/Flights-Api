using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Journey
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public IEnumerable<Flight> Flights { get; set; }

        public Journey()
        {

        }

        public Journey(string origin, string destination, double price, IEnumerable<Flight> flights)
        {

            Origin = origin;
            Destination = destination;
            Price = price;
            Flights = flights;
        }

        //Static Factory Pattern.
        public static Journey Create(string Origin, string Destination, double Price, IEnumerable<Flight> flights)
        {
            return new Journey(Origin, Destination, Price, flights);
        }
    }
}
