namespace DataAccess.Models
{
    public sealed class Flight
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public Transport Transport { get; set; }

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
