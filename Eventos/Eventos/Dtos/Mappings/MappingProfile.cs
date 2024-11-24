using AutoMapper;
using EventosApi.Models;
using EventosApi.Request;

namespace EventosApi.Dtos.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Enterprise, EnterpriseDto>().ReverseMap();
            CreateMap<CreateEventRequest, Event>();
            CreateMap<UpdateEventRequest, Event>();
        }
    }
}
