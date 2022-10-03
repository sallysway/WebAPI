using Application.Data;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;

namespace Application.Baskets.Queries
{
    public class GetBasketContentQueryHandler : IRequestHandler<GetBasketContentQuery, BasketVm>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetBasketContentQueryHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BasketVm> Handle(GetBasketContentQuery request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetBasketAsync(request.Id, cancellationToken);
            if (basket is null)
            {
                throw new CustomException($"Basket with id {request.Id} is not present", "Failed to retrieve basket content");
            }
            var basketVm = _mapper.Map<BasketVm>(basket);
            var articles = await _repository.GetArticles(request.Id, cancellationToken);
            basketVm.Articles = _mapper.Map<List<ArticleVm>>(articles);
            return basketVm;
        }
    }
}
