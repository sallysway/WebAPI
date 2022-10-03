using Application.Entities;

namespace Application.Interfaces
{
    public interface IRepository
    {
        Task<Basket> GetBasketAsync(int id, CancellationToken cancellationToken);

        Task CreateBasketAsync(Basket basket, CancellationToken cancellationToken);
       
        Task UpdateBasketAsync(Basket basket, CancellationToken cancellationToken);

        Task<Basket> GetBasketForCustomerAsync(string customer, CancellationToken cancellationToken);

        Task CreateArticleAsync(int basketId, BasketArticle article, CancellationToken cancellationToken);
        
        Task<List<BasketArticle>> GetArticles(int basketId, CancellationToken cancellationToken);
    }
}
