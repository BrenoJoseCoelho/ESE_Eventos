using EventosApi.Controllers;
using EventosApi.Dtos;
using EventosApi.Request;
using EventosApi.Services.Events;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EventosApi.Tests.Controllers
{
    public class EventControllerTests
    {
        private readonly Mock<IEventService> _mockEventService;
        private readonly EventController _controller;

        public EventControllerTests()
        {
            _mockEventService = new Mock<IEventService>();
            _controller = new EventController(_mockEventService.Object);
        }

        [Fact]
        public async Task Get_Events_ReturnsOkResult_WhenEventsExist()
        {
            // Arrange
            var eventsDto = new List<EventDto>
            {
                new EventDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Event 1",
                    Description = "Description 1",
                    Type = "Type 1",
                    Enterprise = new EnterpriseDto { Id = Guid.NewGuid(), Name = "Enterprise 1", Document = "123456789" }
                },
                new EventDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Event 2",
                    Description = "Description 2",
                    Type = "Type 2",
                    Enterprise = new EnterpriseDto { Id = Guid.NewGuid(), Name = "Enterprise 2", Document = "987654321" }
                }
            };

            _mockEventService.Setup(service => service.GetEvents())
                .ReturnsAsync(eventsDto);

            // Act
            var result = await _controller.Get();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<EventDto>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.NotNull(returnValue[0].Enterprise);  // Verificando se a propriedade Enterprise não é nula
            Assert.Equal("Enterprise 1", returnValue[0].Enterprise.Name);  // Verificando o nome da Enterprise no primeiro evento
        }

        [Fact]
        public async Task Get_EventById_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            _mockEventService.Setup(service => service.GetEventById(eventId))
                .ReturnsAsync((EventDto)null);

            // Act
            var result = await _controller.Get(eventId);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Event not found", actionResult.Value);
        }

        [Fact]
        public async Task Post_Event_CreatesEvent_WhenDataIsValid()
        {
            // Arrange
            var createEventRequest = new CreateEventRequest
            {
                Name = "New Event",
                Description = "New Event Description",
                Type = "Type 1",
                EnterpriseId = Guid.NewGuid() // Supondo que o EnterpriseId é passado aqui no request
            };

            var createdEvent = new EventDto
            {
                Id = Guid.NewGuid(),
                Name = createEventRequest.Name,
                Description = createEventRequest.Description,
                Type = createEventRequest.Type,
                Enterprise = new EnterpriseDto { Id = createEventRequest.EnterpriseId }
            };

            _mockEventService.Setup(service => service.AddEvent(createEventRequest))
                .ReturnsAsync(createdEvent);  // Retorna o evento criado

            // Act
            var result = await _controller.Post(createEventRequest);

            // Assert
            var actionResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetEvent", actionResult.RouteName);
            Assert.Equal(createdEvent.Name, ((EventDto)actionResult.Value).Name);  // A assertiva deve usar o tipo correto
        }

        [Fact]
        public async Task Put_Event_UpdatesEvent_WhenDataIsValid()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var updateEventRequest = new UpdateEventRequest
            {
                Id = eventId,
                Name = "Updated Event",
                Description = "Updated Event Description",
                Type = "Updated Type",
                EnterpriseId = Guid.NewGuid() // Supondo que o EnterpriseId está sendo atualizado
            };

            _mockEventService.Setup(service => service.UpdateEvent(updateEventRequest))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(eventId, updateEventRequest);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UpdateEventRequest>(actionResult.Value);
            Assert.Equal(updateEventRequest.Name, returnValue.Name);
        }

        [Fact]
        public async Task Delete_Event_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            _mockEventService.Setup(service => service.GetEventById(eventId))
                .ReturnsAsync((EventDto)null);  // Simula que o Evento não existe

            // Act
            var result = await _controller.Delete(eventId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<EventDto>>(result);  // Verifique ActionResult<EventDto>
            Assert.IsType<NotFoundObjectResult>(actionResult.Result); // Verifique se é NotFound
        }
    }
}
