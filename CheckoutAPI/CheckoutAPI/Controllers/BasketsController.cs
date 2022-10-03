using Application.Baskets.Commands;
using Application.Baskets.Queries;
using Application.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CheckoutAPI.Controllers
{
    public class BasketsController : ApiController
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get articles in a basket
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetBasket(int id, CancellationToken cancellationToken)
        {
            var getBasketQuery = new GetBasketContentQuery { Id = id };
            var result = await _mediator.Send(getBasketQuery, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a basket for customer
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateBasket(BasketInput basketInput, CancellationToken cancellationToken)
        {
            var createBasketCommand = new CreateBasketCommand { BasketInput = basketInput };
            return Ok(await _mediator.Send(createBasketCommand, cancellationToken));
        }

        /// <summary>
        /// Create article in a basket
        /// </summary>
        [HttpPost("{id}/article-line")]
        public async Task<ActionResult> CreateArticle(int id, ArticleInput articleInput, CancellationToken cancellationToken)
        {
            var createArticleCommand = new CreateArticleCommand {BasketId = id, Article = articleInput };
            return Ok(await _mediator.Send(createArticleCommand, cancellationToken));
        }

        /// <summary>
        /// Update basket
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBasket(int id,BasketInput basketInput, CancellationToken cancellationToken)
        {
            var createBasketCommand = new UpdateBasketCommand { Id = id, BasketInput = basketInput };
            return Ok(await _mediator.Send(createBasketCommand, cancellationToken));
        }
    }
}
