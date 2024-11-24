using AutoMapper;
using EventosApi.Dtos;
using EventosApi.Models;
using EventosApi.Repositories.Events;
using EventosApi.Request;

namespace EventosApi.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventService(IEventRepository EventRepository, IMapper mapper)
        {
            _eventRepository = EventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> GetEvents()
        {
            var eventsEntity = await _eventRepository.GetAll();
            return _mapper.Map<IEnumerable<EventDto>>(eventsEntity);
        }
        public async Task<EventDto> GetEventById(Guid id)
        {
            var eventEntity = await _eventRepository.GetById(id);
            return _mapper.Map<EventDto>(eventEntity);
        }
        public async Task AddEvent(CreateEventRequest eventDto)
        {
            var eventEntity = _mapper.Map<Event>(eventDto);
            await _eventRepository.Create(eventEntity);
        }
        public async Task UpdateEvent(UpdateEventRequest eventDto)
        {
            var eventEntity = _mapper.Map<Event>(eventDto);
            await _eventRepository.Update(eventEntity);
        }
        public async Task RemoveEvent(Guid id)
        {
            var eventEntity = _eventRepository.GetById(id).Result;
            await _eventRepository.Delete(eventEntity.Id);
        }
    }
}
