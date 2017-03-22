namespace CarDealer.Models.ViewModels
{
    public class SaleViewModel
    {
        public CustomerViewModel Customer { get; set; }

        public CarViewModel Car { get; set; }

        public double Discount { get; set; }
    }
}