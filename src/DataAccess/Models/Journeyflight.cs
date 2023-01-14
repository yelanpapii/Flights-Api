using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models
{
    public partial class Journeyflight
    {

        [ForeignKey("Id")]
        public int IdJourney { get; set; }
        [ForeignKey("Id")]
        public int IdFlight { get; set; }

        public virtual Flight IdFlightNavigation { get; set; }
        public virtual Journey IdJourneyNavigation { get; set; }

        public Journeyflight()
        {

        }

        public Journeyflight(int idJourney, Flight flight)
        {
            IdJourney = idJourney;
            IdFlight = flight.Id;
            IdFlightNavigation = flight;
        }

        public static Journeyflight Create(int idJourney, Flight flight)
        {
            return new Journeyflight(idJourney, flight);
        }
    }
}
