using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Journeyflight
    { 

        public int IdJourney { get; set; }
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
