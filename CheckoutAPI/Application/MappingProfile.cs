using Application.Baskets.Commands;
using Application.Entities;
using Application.ViewModels;
using AutoMapper;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Basket, BasketVm>();
            CreateMap<BasketInput, Basket>();
            CreateMap<ArticleInput, Article>();
            CreateMap<Article, ArticleVm>();
            CreateMap<BasketArticle, Article>();

            CreateMap<BasketArticle, ArticleVm>();
                  
            CreateMap<ArticleInput,BasketArticle>()
                .ForMember(d => d.BasketId, opt => opt.Ignore());
        }
    }
}
