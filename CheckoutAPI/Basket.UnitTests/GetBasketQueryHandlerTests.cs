using Application.Entities;
using Application.Baskets.Queries;
using Moq;
using Xunit;
using Application.Interfaces;
using AutoFixture;

namespace Basket.UnitTests
{
    public class GetBasketQueryHandlerTests
    {
        private readonly Mock<IRepository> _repositoryMock = new();
        private GetBasketContentQueryHandler _getBasketQueryHandler;
        private readonly Fixture _fixture = new();
        public GetBasketQueryHandlerTests()
        {
            var mapper = Util.BuildMapper();
            _getBasketQueryHandler = new GetBasketContentQueryHandler(_repositoryMock.Object, mapper);
        }
        [Fact]
        public async Task GetBasket_Returns_Stored_Basket()
        {
            //Arrange
            var basketId = 123;
            var query = new GetBasketContentQuery { Id = basketId };
            var articles = _fixture.CreateMany<BasketArticle>(5).ToList();
            var foundBasket = new Application.Entities.Basket { Id = basketId};
            _repositoryMock.Setup(r => r.GetBasketAsync(It.Is<int>(i => i == basketId), CancellationToken.None))
                .ReturnsAsync(foundBasket);
            _repositoryMock.Setup(r => r.GetArticles(It.Is<int>(i => i == basketId), CancellationToken.None))
               .ReturnsAsync(articles);

            //Act
            var result = await _getBasketQueryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(basketId, result.Id);
            Assert.Equal(articles.Count(), result.Articles.Count());
        }
    }
}