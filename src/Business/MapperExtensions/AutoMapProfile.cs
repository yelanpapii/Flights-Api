using AutoMapper;
using DataAccess.Models;

namespace Business.MapperExtensions
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<JourneyDTO, Journey>().ReverseMap();
            CreateMap<FlightDTO, Flight>().ReverseMap();
            CreateMap<TransportDTO, Transport>().ReverseMap();
        }
    }
}
