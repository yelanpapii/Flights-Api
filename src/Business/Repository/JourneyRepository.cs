using Business.Repository.Interface;
using DataAccess.Models;
using DataAccess.Persistence.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly IAppContext _context;

        public JourneyRepository(IAppContext context)
        {
            _context = context;
        }

        public async Task<Journey> GetJourney(string origin, string destination)
        {
            return await _context.Journeys
                .Include(x => x.Flights).ThenInclude(z => z.Transport).FirstOrDefaultAsync(x => x.Origin == origin && x.Destination == destination);
        }

        public async Task CreateJourney(Journey journey)
        {
            if (!await _context.Journeys.ContainsAsync(journey))
            {
                await _context.Journeys.AddAsync(journey);
                await _context.SaveChangesAsync(System.Threading.CancellationToken.None);
            }

        }

        public async Task CreateJourney(List<Journey> journey)
        {
            foreach (var item in journey)
            {
                if (!await _context.Journeys.ContainsAsync(item))
                {
                    await _context.Journeys.AddAsync(item);
                    await _context.SaveChangesAsync(System.Threading.CancellationToken.None);
                }
            }

        }
    }
}
