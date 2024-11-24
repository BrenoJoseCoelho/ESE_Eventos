using EventosApi.Dtos;
using EventosApi.Request;

namespace EventosApi.Services.Events
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetEvents();
        Task<EventDto> GetEventById(Guid id);
        Task AddEvent(CreateEventRequest eventDto);
        Task UpdateEvent(UpdateEventRequest eventDto);
        Task RemoveEvent(Guid id);
    }
}
