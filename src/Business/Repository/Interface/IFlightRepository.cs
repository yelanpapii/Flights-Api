using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.Interface
{
    public interface IFlightRepository
    {
        Task CreateFlight(Flight flight);
        Task CreateFlight(List<Flight> flight);
        Task<Flight> GetFlight(int id);
    }
}