
using Application.Baskets.Commands;
using Application.Entities;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Basket.UnitTests
{
    public class UpdateBasketCommandHandlerTests
    {
        private UpdateBasketCommandHandler _updateBasketCommandHandler;
        private readonly Mock<IRepository> _mockRepository = new();
        private readonly Mock<ILogger<UpdateBasketCommandHandler>> _mockLogger = new();
        public UpdateBasketCommandHandlerTests()
        {
            var mapper = Util.BuildMapper();
            _updateBasketCommandHandler = new UpdateBasketCommandHandler(_mockRepository.Object, mapper, _mockLogger.Object);
        }

        [Fact]
        public async Task Basket_Is_Updated_With_New_Values()
        {
            //Arrange
            var basketId = 123;
            var basketInput = new BasketInput { Customer = "John Smith", PaysVAT = true, Status = Status.Closed };
            var updateBasketCommand = new UpdateBasketCommand { Id = basketId, BasketInput = basketInput };
            var foundBasket = new Application.Entities.Basket { Id = basketId };
            _mockRepository.Setup(r => r.GetBasketAsync(It.Is<int>(i => i == basketId), CancellationToken.None))
                .ReturnsAsync(foundBasket);

            //Act
            await _updateBasketCommandHandler.Handle(updateBasketCommand, CancellationToken.None);

            //Assert
            _mockRepository.Verify(r => r.UpdateBasketAsync(It.Is<Application.Entities.Basket>(b => b.Id == updateBasketCommand.Id),
                CancellationToken.None), Times.Once);

        }
    }
}
