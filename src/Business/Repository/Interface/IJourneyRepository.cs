using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.Interface
{
    public interface IJourneyRepository
    {
        Task CreateJourney(Journey journey);
        Task CreateJourney(List<Journey> journey);
        Task<Journey> GetJourney(string origin, string destination);
    }
}