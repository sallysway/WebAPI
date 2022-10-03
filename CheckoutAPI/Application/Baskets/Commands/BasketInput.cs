using Application.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Baskets.Commands
{
    public class BasketInput
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Customer { get; set; }

        public bool PaysVAT { get; set; }

        public Status Status { get; set; }
    }
}
      