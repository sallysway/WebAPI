namespace Application.ViewModels
{
    public class BasketVm
    {
        const int DEFAULT_VAT = 10;
        
        public int Id { get; set; }

        public IEnumerable<ArticleVm> Articles { get; set; }

        public int TotalNet {
            get { return CalculateTotalNet(); }
        }

        public int TotalGross { get { return Articles.Sum(a => a.Price); } }

        public string Customer { get; set; }

        public bool PaysVAT { get; set; }

        private int CalculateTotalNet()
        {
            var totalPriceOfBasket = Articles.Sum(a => a.Price);
            return totalPriceOfBasket + (totalPriceOfBasket / DEFAULT_VAT);
        }
    }
}
