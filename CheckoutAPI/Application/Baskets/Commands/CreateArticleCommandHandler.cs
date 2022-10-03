using Application.Data;
using Application.Entities;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Baskets.Commands
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateArticleCommandHandler> _logger;

        public CreateArticleCommandHandler(
            IRepository repository, 
            IMapper mapper, 
            ILogger<CreateArticleCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var storedBasket = await _repository.GetBasketAsync(request.BasketId, cancellationToken);
            if (storedBasket is null)
            {
                throw new CustomException($"Basket with id {request.BasketId} is not present", "Failed to create basket");
            }
            if(storedBasket.Status is not Status.Active)
            {
                throw new CustomException($"Basket with id {request.BasketId} is not active", "Articles cannot be added to an inactive basket");

            }
            var articleToAdd = _mapper.Map<BasketArticle>(request.Article);
            articleToAdd.BasketId = request.BasketId;
            await _repository.CreateArticleAsync(request.BasketId, articleToAdd, cancellationToken);

            return Unit.Value;
        }
    }
}
