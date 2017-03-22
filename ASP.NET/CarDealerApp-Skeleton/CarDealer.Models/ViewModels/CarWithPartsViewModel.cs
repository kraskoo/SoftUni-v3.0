namespace CarDealer.Models.ViewModels
{
    using System.Collections.Generic;

    public class CarWithPartsViewModel : CarViewModel
    {
        public IEnumerable<PartViewModel> Parts { get; set; }
    }
}