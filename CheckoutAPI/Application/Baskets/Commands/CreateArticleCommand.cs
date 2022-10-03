using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Baskets.Commands
{
    public class CreateArticleCommand : IRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int BasketId { get; set; }

        public ArticleInput Article { get ; set; }
    }
}
