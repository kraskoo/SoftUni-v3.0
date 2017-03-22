namespace Application.Controllers
{
    using System.Collections.Generic;
    using Data;
    using Data.Interfaces;
    using Data.Services;
    using Models.BindingModels;
    using Models.ViewModels;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Controllers;
    using SimpleMVC.Interfaces.Generic;
    using Utilities;

    public class HomeController : Controller
    {
        private readonly IDataProvidable data;
        private readonly HomeService homeService;

        public HomeController(IDataProvidable data)
        {
            this.data = data;
            this.homeService = new HomeService(data);
        }

        public HomeController() : this(new SoftUniData(new SoftUniStoreContext()))
        {
        }

        [HttpGet]
        public IActionResult<IEnumerable<HomeGameViewModel>> Index(
            HttpSession session, HttpResponse response, string filter)
        {
            var isAuthenticated = session.IsUserAuthenticated(this.data);
            this.SetupNavAndHome(isAuthenticated);
            if (!isAuthenticated)
            {
                return this.View(this.homeService.Games(null, filter));
            }

            var user = this.homeService.FindUserBySession(session);
            this.homeService.SetupNavbar(user);
            return this.View(this.homeService.Games(user, filter));
        }

        [HttpGet]
        public IActionResult<DetailsGameViewModel> Details(
            HttpSession session, HttpResponse response, int id)
        {
            var user = session.GetAuthenticatedUser(this.data);
            this.SetupNavAndHome(user != null);
            if (user != null)
            {
                this.homeService.SetupNavbar(user);
            }
            
            this.SetGameDetails(this.homeService, user, id);
            var game = this.data.Games.Find(id);
            if (game == null)
            {
                this.Redirect(response, "/home/index");
                return null;
            }

            return this.View(this.homeService.GameDetails(id));
        }

        [HttpPost]
        public void Details(HttpResponse response, BuyGameBindingModel bgbm)
        {
            this.homeService.BuyGame(bgbm);
            this.SetGameDetails(this.homeService, this.data.Users.Find(bgbm.UserId), bgbm.GameId);
            this.Redirect(response, "/");
        }
    }
}