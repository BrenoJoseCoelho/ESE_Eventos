using EventosApi.Dtos;
using EventosApi.Request;
using EventosApi.Services.Events;
using Microsoft.AspNetCore.Mvc;

namespace EventosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _EventService;

        public EventController(IEventService EventService)
        {
            _EventService = EventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> Get()
        {
            var EventsDto = await _EventService.GetEvents();

            if (EventsDto is null)
                return NotFound("Events not found");

            return Ok(EventsDto);
        }

        [HttpGet("{id:Guid}", Name = "GetEvent")]
        public async Task<ActionResult<EventDto>> Get(Guid id)
        {
            var EventDto = await _EventService.GetEventById(id);
            if (EventDto == null)
            {
                return NotFound("Event not found");
            }
            return Ok(EventDto);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateEventRequest eventrequest)
        {
            if (eventrequest == null)
                return BadRequest("Invalid Data");

            await _EventService.AddEvent(eventrequest);

            return new CreatedAtRouteResult("GetEvent",
                eventrequest);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateEventRequest updateEventRequest)
        {
            if (id != updateEventRequest.Id)
                return BadRequest();

            if (updateEventRequest is null)
                return BadRequest();

            await _EventService.UpdateEvent(updateEventRequest);

            return Ok(updateEventRequest);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<EventDto>> Delete(Guid id)
        {
            var EventDto = await _EventService.GetEventById(id);
            if (EventDto == null)
            {
                return NotFound("Event not found");
            }

            await _EventService.RemoveEvent(id);

            return Ok(EventDto);
        }
    }
}
