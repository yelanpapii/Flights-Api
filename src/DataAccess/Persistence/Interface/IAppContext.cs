
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Persistence.Interface
{
    public interface IAppContext
    {
        DbSet<Flight> Flights { get; set; }
        DbSet<Journey> Journeys { get; set; }
        DbSet<Transport> Transports { get; set; }
        DbSet<Journeyflight> Journeyflights { get; set; }
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}