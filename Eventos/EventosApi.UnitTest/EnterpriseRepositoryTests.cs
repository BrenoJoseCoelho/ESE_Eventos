using EnterpriseosApi.Controllers;
using EventosApi.Dtos;
using EventosApi.Services.Enterprises;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EventosApi.Tests.Controllers
{
    public class EnterpriseControllerTests
    {
        private readonly Mock<IEnterpriseService> _mockEnterpriseService;
        private readonly EnterpriseController _controller;

        public EnterpriseControllerTests()
        {
            _mockEnterpriseService = new Mock<IEnterpriseService>();
            _controller = new EnterpriseController(_mockEnterpriseService.Object);
        }

        [Fact]
        public async Task Get_Enterprises_ReturnsOkResult_WhenEnterprisesExist()
        {
            // Arrange
            var enterprisesDto = new List<EnterpriseDto>
            {
                new EnterpriseDto { Id = Guid.NewGuid(), Name = "Enterprise 1", Document = "123456" },
                new EnterpriseDto { Id = Guid.NewGuid(), Name = "Enterprise 2", Document = "654321" }
            };

            _mockEnterpriseService.Setup(service => service.GetEnterprises())
                .ReturnsAsync(enterprisesDto);

            // Act
            var result = await _controller.Get();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<EnterpriseDto>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task Get_EnterpriseById_ReturnsNotFound_WhenEnterpriseDoesNotExist()
        {
            // Arrange
            var enterpriseId = Guid.NewGuid();
            _mockEnterpriseService.Setup(service => service.GetEnterpriseById(enterpriseId))
                .ReturnsAsync((EnterpriseDto)null);

            // Act
            var result = await _controller.Get(enterpriseId);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Enterprise not found", actionResult.Value);
        }

        [Fact]
        public async Task Post_Enterprise_CreatesEnterprise_WhenDataIsValid()
        {
            // Arrange
            var enterpriseDto = new EnterpriseDto { Id = Guid.NewGuid(), Name = "New Enterprise", Document = "123123" };

            _mockEnterpriseService.Setup(service => service.AddEnterprise(enterpriseDto))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(enterpriseDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnValue = Assert.IsType<EnterpriseDto>(actionResult.Value);
            Assert.Equal(enterpriseDto.Name, returnValue.Name);
        }

        [Fact]
        public async Task Put_Enterprise_UpdatesEnterprise_WhenDataIsValid()
        {
            // Arrange
            var enterpriseId = Guid.NewGuid();
            var enterpriseDto = new EnterpriseDto { Id = enterpriseId, Name = "Updated Enterprise", Document = "111111" };

            _mockEnterpriseService.Setup(service => service.UpdateEnterprise(enterpriseDto))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(enterpriseId, enterpriseDto);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<EnterpriseDto>(actionResult.Value);
            Assert.Equal(enterpriseDto.Name, returnValue.Name);
        }
        [Fact]
        public async Task Delete_Enterprise_ReturnsNotFound_WhenEnterpriseDoesNotExist()
        {
            // Arrange
            var enterpriseId = Guid.NewGuid();
            _mockEnterpriseService.Setup(service => service.GetEnterpriseById(enterpriseId))
                .ReturnsAsync((EnterpriseDto)null);  // Simula que o Enterprise não existe

            // Act
            var result = await _controller.Delete(enterpriseId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<EnterpriseDto>>(result);  // Verifique ActionResult<EnterpriseDto>
            Assert.IsType<NotFoundObjectResult>(actionResult.Result); // Verifique se é NotFound
        }
    }
}
