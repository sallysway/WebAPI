using MediatR;

namespace Application.Baskets.Commands
{
    public class UpdateBasketCommand : IRequest
    {
        public BasketInput BasketInput { get; set; }
        public int Id { get; set; }
    }
}
