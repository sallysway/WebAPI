
namespace Application.Entities
{
    public class BasketArticle
    {
        public int Id { get; set; }

        public int BasketId { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }
    }
}
