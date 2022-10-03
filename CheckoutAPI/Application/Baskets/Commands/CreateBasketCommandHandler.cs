using Application.Data;
using Application.Entities;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Baskets.Commands
{
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBasketCommandHandler> _logger;

        public CreateBasketCommandHandler(
            IRepository repository, 
            IMapper mapper, 
            ILogger<CreateBasketCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = _mapper.Map<Basket>(request.BasketInput);

            var basketForCustomer = await _repository.GetBasketForCustomerAsync(basket.Customer, cancellationToken);
            if (basketForCustomer is not null && basketForCustomer.Status is Status.Active)
            {
                throw new CustomException($"Customer {basket.Customer} has an active basket with id {basket.Id} ",
                    "Only one active basket allowed for a customer");
            }
            await _repository.CreateBasketAsync(basket, cancellationToken);

            return Unit.Value;
        }
    }
}
