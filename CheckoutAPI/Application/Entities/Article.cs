
namespace Application.Entities
{
    public class Article
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public ProductCategory ProductCategory { get; set; }
    }
}
