using Application.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<Repository> _logger;

        public Repository(DataContext context, ILogger<Repository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<Basket> GetBasketAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving basket with id {basketId}", id);
            Basket basket = null;
            try
            {
                basket = await _context.Baskets.SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new CustomException($"Failed to retrieve basket with id {id}.", $"Error: {ex}");
            }
            _context.ChangeTracker.Clear();
            _context.SaveChangesAsync();
            return basket;
        }

        public async Task<Basket> GetBasketForCustomerAsync(string customer, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving basket  for customer {customer}", customer);
            Basket basketForCustomer = null;
            try
            {
                basketForCustomer = await _context.Baskets.SingleOrDefaultAsync(b => b.Customer.ToLower() == customer.ToLower(), 
                    cancellationToken);
                
                return basketForCustomer;
            }
            catch (Exception ex)
            {
                throw new CustomException($"Failed to retrieve basket for customer {customer}.", $"Error: {ex}");
            }
        }


        public async Task CreateBasketAsync(Basket basket, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new basket for customer {customer}", basket.Customer);

            await _context.Baskets.AddAsync(basket, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBasketAsync(Basket basket, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating basket for customer {customer}", basket.Customer);
            var storedBasket = await _context.Baskets.SingleOrDefaultAsync(b => b.Id == basket.Id, cancellationToken);
            _context.Entry(storedBasket).CurrentValues.SetValues(basket);
            await _context.SaveChangesAsync();
        }

        public async Task CreateArticleAsync(int basketId, BasketArticle article, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating article in basket with id {basketId}", basketId);
            
            await _context.BasketArticles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BasketArticle>> GetArticles(int basketId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving articles in basket with id {basketId}", basketId);

            return await _context.BasketArticles.Where(a => a.BasketId == basketId).ToListAsync();
        }
    }
}
