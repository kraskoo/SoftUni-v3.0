namespace CarDealer.Models.ViewModels
{
    using System.Collections.Generic;

    public class SalesViewModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public IEnumerable<SoldCarViewModel> SoldCars { get; set; }
    }
}