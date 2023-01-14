using Business.Repository.Interface;
using DataAccess.Models;
using DataAccess.Persistence.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IAppContext _context;

        public FlightRepository(IAppContext context)
        {
            _context = context;
        }

        public async Task<Flight> GetFlight(int id)
        {
            return await _context.Flights.Where(x => x.Id == id)
                .Include(x => x.Transport).FirstOrDefaultAsync();
        }
        public async Task CreateFlight(Flight flight)
        {
            if (!await _context.Flights.ContainsAsync(flight))
            {
                await _context.Flights.AddAsync(flight);
                await _context.SaveChangesAsync(System.Threading.CancellationToken.None);
            }

        }

        public async Task CreateFlight(List<Flight> flight)
        {
            foreach (var item in flight)
            {
                if (!await _context.Flights.ContainsAsync(item))
                    await _context.Flights.AddAsync(item);
                await _context.SaveChangesAsync(System.Threading.CancellationToken.None);

            }

        }
    }
}
