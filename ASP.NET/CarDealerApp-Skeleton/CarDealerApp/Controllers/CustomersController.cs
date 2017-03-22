namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using CarDealer.Services;

    public class CustomersController : Controller
    {
        private readonly CustomersService customersService;

        public CustomersController()
        {
            this.customersService = new CustomersService();
        }

        [HttpGet]
        public ActionResult All(string order)
        {
            return this.View(this.customersService.GetCustomersInOrder(order));
        }

        [HttpGet]
        public ActionResult SalesByCustomer(int id)
        {
            return this.View(this.customersService.GetSalesByCustomerId(id));
        }
    }
}