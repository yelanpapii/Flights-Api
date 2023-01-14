using System.Threading.Tasks;

namespace Business.Services.Interface
{
    public interface IJourneyService
    {
        Task<JourneyDTO> GetJourneyAsync(string origin, string destination);
        Task<JourneyDTO> GetJourneyFromDbAsync(string origin, string destination);
    }
}