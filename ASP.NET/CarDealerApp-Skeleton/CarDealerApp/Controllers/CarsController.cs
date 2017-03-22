namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using CarDealer.Services;

    public class CarsController : Controller
    {
        private readonly CarsService carsService;

        public CarsController()
        {
            this.carsService = new CarsService();
        }

        [HttpGet]
        public ActionResult All()
        {
            return this.View(this.carsService.GetAllCars());
        }

        [HttpGet]
        public ActionResult Make(string make)
        {
            return this.View(this.carsService.GetCarFromMake(make));
        }

        [HttpGet]
        public ActionResult Parts(int id)
        {
            return this.View(this.carsService.GetCarWithPars(id));
        }
    }
}