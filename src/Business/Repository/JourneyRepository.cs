using Business.Repository.Interface;
using DataAccess.Models;
using DataAccess.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

        public async Task<Journey> GetJourney(int id)
        {
            return await _context.Journeys.Where(x => x.Id == id)
                .Include(x => x.Flights).FirstOrDefaultAsync();
        }

        public async Task CreateJourney(Journey journey)
        {
            await _context.Journeys.AddAsync(journey);
            await _context.SaveChangesAsync(System.Threading.CancellationToken.None);
        }

        public async Task CreateJourney(List<Journey> journey)
        {
            await _context.Journeys.AddRangeAsync(journey);
            await _context.SaveChangesAsync(System.Threading.CancellationToken.None);
        }
    }
}
