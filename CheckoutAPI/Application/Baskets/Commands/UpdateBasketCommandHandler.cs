using Application.Data;
using Application.Entities;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Baskets.Commands
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBasketCommandHandler> _logger;
        public UpdateBasketCommandHandler(
            IRepository repository,
            IMapper mapper, 
            ILogger<UpdateBasketCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<Unit> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = _mapper.Map<Basket>(request.BasketInput);
            var storedBasket = await _repository.GetBasketAsync(request.Id, cancellationToken);
            if (storedBasket is null)
            {
                throw new CustomException($"Basket with id {request.Id} is not present", "Cannot update basket");
            }
            basket.Id = request.Id;
            await _repository.UpdateBasketAsync(basket, cancellationToken);

            return Unit.Value;
        }
    }
}
