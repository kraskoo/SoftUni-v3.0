namespace CarDealer.Models.ViewModels
{
    public class SoldCarViewModel
    {
        public string MakeAndModel { get; set; }

        public double Discount { get; set; }

        public decimal PriceWithoutDiscount { get; set; }

        public decimal PriceWithDiscount { get; set; }
    }
}