
using Application.Baskets.Commands;
using Application.Data;
using Application.Entities;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Basket.UnitTests
{
    public class CreateArticleCommandHandlerTests
    {
        private CreateArticleCommandHandler _createArticleCommandHandler;
        private readonly Mock<IRepository> _mockRepository = new();
        private readonly Mock<ILogger<CreateArticleCommandHandler>> _mockLogger = new();
        public CreateArticleCommandHandlerTests()
        {
            var mapper = Util.BuildMapper();
            _createArticleCommandHandler = new CreateArticleCommandHandler(_mockRepository.Object, mapper, _mockLogger.Object);
        }

        [Theory]
        [InlineData(123,Status.Closed)]
        [InlineData(123,Status.Expired)]
        [InlineData(-1,Status.Active)]
        public async Task Create_Article_For_Inactive_Or_Inexistent_Basket_Throws_Exception(int basketId, Status status)
        {
            //Arrange
            var storedBasket = new Application.Entities.Basket { Status = status };
            _mockRepository.Setup(r => r.GetBasketAsync(basketId,CancellationToken.None))
                .ReturnsAsync(storedBasket);
            var article = new ArticleInput { Description = "bread", Price = 100 };
            var createArticleCommand = new CreateArticleCommand { Article = article, BasketId = basketId };

            //Act
            Func<Task> handler = async () => await _createArticleCommandHandler.Handle(createArticleCommand, CancellationToken.None);

            //Assert
            handler.Should().ThrowAsync<CustomException>();
        }

        [Theory]
        [InlineData("",0)]
        [InlineData("to",30)]
        [InlineData("string with more than 30 characters ", 40)]
        [InlineData("valid description ", 100000)]
        public async Task Create_Article_Throws_Exception_On_Invalid_Input_Data(string description, int price)
        {
            //Arrange
            var basketId = 1;
            var storedBasket = new Application.Entities.Basket { Status = Status.Active };
            _mockRepository.Setup(r => r.GetBasketAsync(basketId, CancellationToken.None))
                .ReturnsAsync(storedBasket);
            var article = new ArticleInput { Description = "bread", Price = 100 };
            var createArticleCommand = new CreateArticleCommand { Article = article, BasketId = basketId };

            //Act
            Func<Task> handler = async () => await _createArticleCommandHandler.Handle(createArticleCommand, CancellationToken.None);

            //Assert
            handler.Should().ThrowAsync<Exception>();
        }
    }
}
