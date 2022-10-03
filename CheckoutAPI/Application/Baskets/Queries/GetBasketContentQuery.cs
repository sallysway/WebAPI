using Application.ViewModels;
using MediatR;

namespace Application.Baskets.Queries
{
    public class GetBasketContentQuery : IRequest<BasketVm>
    {
        public int Id { get; set; }
    }
}
