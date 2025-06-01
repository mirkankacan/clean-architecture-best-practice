using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetCarById;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest
{
    public class CarsControllerUnitTest
    {
        [Fact]
        public async Task Create_ReturnsOkResult_WhenRequestIsValid()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            CreateCarCommand createCarCommand = new CreateCarCommand("Toyota", "Corolla", 5000);
            CreateCarCommandResponse createCarCommandResponse = new(Guid.NewGuid().ToString());
            CancellationToken cancellationToken = new CancellationToken();
            mediatorMock.Setup(m => m.Send(createCarCommand, cancellationToken))
                .ReturnsAsync(createCarCommandResponse);
            CarsController carsController = new(mediatorMock.Object);
            // Act
            var result = await carsController.Create(createCarCommand, cancellationToken);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreateCarCommandResponse>(okResult.Value);
            Assert.Equal(createCarCommandResponse, returnValue);
            mediatorMock.Verify(m => m.Send(createCarCommand, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenRequestIsValid()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            GetCarByIdQuery getCarByIdQuery = new GetCarByIdQuery(Guid.NewGuid().ToString());
            GetCarByIdQueryResponse getCarByIdQueryResponse = new(new Domain.Entities.Car()
            {
                Name = "Toyota",
                Model = "Corolla",
                EnginePower = 5000,
            });
            CancellationToken cancellationToken = new CancellationToken();
            mediatorMock.Setup(m => m.Send(getCarByIdQuery, cancellationToken))
                .ReturnsAsync(getCarByIdQueryResponse);
            CarsController carsController = new(mediatorMock.Object);
            // Act
            var result = await carsController.GetById(Guid.NewGuid().ToString(), cancellationToken);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetCarByIdQueryResponse>(okResult.Value);
            Assert.Equal(getCarByIdQueryResponse, returnValue);
            mediatorMock.Verify(m => m.Send(getCarByIdQuery, cancellationToken), Times.Once);
        }
    }
}