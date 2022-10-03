
using Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities
{
    [Table("Basket")]
    public class Basket
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public string Customer { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool PaysVAT { get; set; }

        public IList<Article> Articles { get; set; } = new List<Article>();

        public bool IsExpired()
        {
            return (DateTime.UtcNow - CreatedDate).Hours > Constants.ExpirationPeriodInHours;
        }
    }
}
