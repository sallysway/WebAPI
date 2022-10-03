using MediatR;

namespace Application.Baskets.Commands
{
    public class CreateBasketCommand : IRequest
    {
        public BasketInput BasketInput { get; set; }
    }
}
