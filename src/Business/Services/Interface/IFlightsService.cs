using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interface
{
    public interface IFlightsService
    {
        
        Task<IEnumerable<FlightDTO>> GetAllFlightAsync(string origin, string destination);
    }
}