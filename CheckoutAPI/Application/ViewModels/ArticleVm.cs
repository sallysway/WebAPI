using System.Text.Json.Serialization;

namespace Application.ViewModels
{
    public class ArticleVm
    {
        [JsonPropertyName("article")]
        public string Description { get; set; }

        public int Price { get; set; }
    }
}
