namespace DataAccess.Models
{
    public class Flight
    {
        public Transport Transport { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }

        public Flight(Transport transport, string origin, string destination, double price)
        {
            Transport = transport;
            Origin = origin;
            Destination = destination;
            Price = price;
        }

        public static Flight Create(Transport transport, string Origin, string Destination, double Price)
        {
            return new Flight(transport, Origin, Destination, Price);
        }
    }
}
