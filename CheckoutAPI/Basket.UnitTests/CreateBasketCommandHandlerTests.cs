using Application.Baskets.Commands;
using Application.Data;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Basket.UnitTests
{
    public class CreateBasketCommandHandlerTests
    {
        private CreateBasketCommandHandler _createBasketCommandHandler;
        private readonly Mock<IRepository> _mockRepository = new();
        private readonly Mock<ILogger<CreateBasketCommandHandler>> _mockLogger= new();
        public CreateBasketCommandHandlerTests()
        {
            var mapper = Util.BuildMapper();
            _createBasketCommandHandler = new CreateBasketCommandHandler(_mockRepository.Object, mapper, _mockLogger.Object);
        }

        [Theory]
        [InlineData("John Smith")]
        [InlineData("JOhn smith")]
        [InlineData("john smith")]
        public async Task CreateBasket_Does_Not_Add_Second_Basket_For_Customer(string customerName)
        {
            //Arrange
            var basketInput = new BasketInput { Customer = customerName, PaysVAT = true };
            var createBasketCommand = new CreateBasketCommand { BasketInput = basketInput };
            var storedBasket = new Application.Entities.Basket { Customer = customerName, Id = 1 };
            _mockRepository.Setup(r => r.GetBasketForCustomerAsync(customerName, CancellationToken.None))
                .ReturnsAsync(storedBasket);

            //Act
            Func<Task> handler = async () => await _createBasketCommandHandler.Handle(createBasketCommand, CancellationToken.None);

            //Assert
            handler.Should().ThrowAsync<CustomException>();
            _mockRepository.Verify(r => r.CreateBasketAsync(It.Is<Application.Entities.Basket>(b => b.Customer == customerName), 
                CancellationToken.None), Times.Never);
                
        }

        [Theory]
        [InlineData("LArry David")]
        [InlineData("larry David")]
        [InlineData("Larry david")]
        public async Task CreateBasket_Adds_Basket_For_Customer(string customerName)
        {
            //Arrange
            var basketInput = new BasketInput { Customer = customerName, PaysVAT = true };
            var createBasketCommand = new CreateBasketCommand { BasketInput = basketInput };
            Application.Entities.Basket storedBasket = null;
            _mockRepository.Setup(r => r.GetBasketForCustomerAsync(customerName, CancellationToken.None))
               .ReturnsAsync(storedBasket);

            //Act
            await _createBasketCommandHandler.Handle(createBasketCommand, CancellationToken.None);

            //Assert
            _mockRepository.Verify(r => r.CreateBasketAsync(It.Is<Application.Entities.Basket>(b => b.Customer == customerName), 
                CancellationToken.None), Times.Once);

        }
    }
}



