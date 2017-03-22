namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using CarDealer.Services;

    public class SalesController : Controller
    {
        private readonly SalesService salesService;

        public SalesController()
        {
            this.salesService = new SalesService();
        }

        [HttpGet]
        public ActionResult GetAllSales()
        {
            return this.View(this.salesService.GetAllSales());
        }

        [HttpGet]
        public ActionResult SaleById(int id)
        {
            return this.View(this.salesService.SaleById(id));
        }

        [HttpGet]
        public ActionResult GetDiscounted(int? percentage)
        {
            if (percentage != null)
            {
                return this.View(this.salesService.AllDiscountedSales(percentage.GetValueOrDefault()));
            }

            return this.View(this.salesService.AllDiscountedSales());
        }
    }
}