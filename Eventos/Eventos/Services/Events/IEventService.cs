using EventosApi.Dtos;
using EventosApi.Request;

namespace EventosApi.Services.Events
{

    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetEvents(); // Lista de eventos
        Task<EventDto> GetEventById(Guid id); // Buscar evento por ID
        Task<EventDto> AddEvent(CreateEventRequest eventDto); // Adicionar novo evento
        Task UpdateEvent(UpdateEventRequest eventDto); // Atualizar evento existente
        Task<EventDto> RemoveEvent(Guid id); // Remover evento por ID
    }
}
