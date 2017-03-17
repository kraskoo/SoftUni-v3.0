namespace Application.Controllers
{
    using Data;
    using Data.Interfaces;
    using Data.Services;
    using Models.BindingModels;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Controllers;
    using SimpleMVC.Interfaces;
    using Utilities;

    public class UsersController : Controller
    {
        private readonly IDataProvidable data;
        private readonly UserService userService;

        public UsersController(IDataProvidable dataProvider)
        {
            this.data = dataProvider;
            this.userService = new UserService(this.data);
        }

        public UsersController() : this(new SoftUniData(new SoftUniStoreContext()))
        {
        }

        [HttpGet]
        public IActionResult Register(HttpSession session, HttpResponse response)
        {
            var isAuthenticated = session.IsUserAuthenticated(data);
            this.SetupNavAndHome(isAuthenticated);
            if (isAuthenticated)
            {
                this.Redirect(response, "/");
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public void Register(HttpSession session, HttpResponse response, RegistrationUserBindingModel rubm)
        {
            var isAuthenticated = session.IsUserAuthenticated(data);
            this.SetupNavAndHome(isAuthenticated);
            bool isValid = this.userService.IsRegistrationValid(rubm);
            if (isAuthenticated)
            {
                this.Redirect(response, "/");
                return;
            }

            this.userService.RegisterUser(rubm);
            this.Redirect(response, isValid ? "/users/login" : "/");
        }

        [HttpGet]
        public IActionResult Login(HttpSession session, HttpResponse response)
        {
            var isAuthenticated = session.IsUserAuthenticated(data);
            this.SetupNavAndHome(isAuthenticated);
            if (isAuthenticated)
            {
                this.Redirect(response, "/");
                return null;
            }

            var user = this.userService.FindUserBySession(session);
            //this.SetupUserDependant(isAuthenticated, user);
            return this.View();
        }

        [HttpPost]
        public void Login(HttpSession session, HttpResponse response, LoginUserBindingModel lubm)
        {
            var isAuthenticated = session.IsUserAuthenticated(data);
            if (isAuthenticated)
            {
                this.Redirect(response, "/");
                return;
            }

            var isLoginValid = this.userService.IsLoginValid(lubm, session.Id);
            var user = this.userService.FindUserBySession(session);
            this.SetupNavAndHome(user != null);
            this.Redirect(response, isLoginValid ? "/" : "/users/login");
        }

        [HttpGet]
        public void Logout(HttpSession session, HttpResponse response)
        {
            if (!session.IsUserAuthenticated(data))
            {
                this.Redirect(response, "/users/login");
            }

            session.Logout(data, response);
            this.Redirect(response, "/users/login");
        }
    }
}