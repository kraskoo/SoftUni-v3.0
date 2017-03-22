namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using CarDealer.Services;

    public class SuppliersController : Controller
    {
        private readonly SuppliersService service;

        public SuppliersController()
        {
            this.service = new SuppliersService();
        }

        [HttpGet]
        public ActionResult GetSuppliers(string type)
        {
            return View(this.service.GetSupplier(type));
        }
    }
}