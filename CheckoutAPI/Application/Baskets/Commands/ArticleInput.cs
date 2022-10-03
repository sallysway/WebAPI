using System.ComponentModel.DataAnnotations;

namespace Application.Baskets.Commands
{
    public class ArticleInput
    {
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Range(1, 100000, ErrorMessage = "Please enter a valid price")]
        public int Price { get; set; }
   }
}
