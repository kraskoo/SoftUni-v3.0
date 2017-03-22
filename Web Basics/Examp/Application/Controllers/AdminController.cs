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

    public class AdminController : Controller
    {
        private readonly IDataProvidable data;
        private readonly AdminService adminService;

        public AdminController(IDataProvidable data)
        {
            this.data = data;
            this.adminService = new AdminService(this.data);
        }

        public AdminController() : this(new SoftUniData(new SoftUniStoreContext()))
        {
        }

        [HttpGet]
        public IActionResult<AddGameViewModel> Add(HttpSession session, HttpResponse response)
        {
            var isAuthenticated = session.IsUserAuthenticated(this.data);
            var user = this.adminService.FindUserBySession(session);
            bool isUserInAdmin = false;
            if (user != null)
            {
                isUserInAdmin = this.adminService.IsUserInAdminRole(user);
            }

            if (!isAuthenticated || !isUserInAdmin)
            {
                this.Redirect(response, "/");
                return null;
            }

            this.SetupNavAndHome(true);
            this.adminService.SetupNavbar(user);
            return this.View(this.adminService.GetAddGame());
        }

        [HttpPost]
        public void Add(HttpSession session, HttpResponse response, AddGameBindingModel agbm)
        {
            var isAuthenticated = session.IsUserAuthenticated(this.data);
            var user = this.adminService.FindUserBySession(session);
            bool isUserInAdmin = false;
            if (user != null)
            {
                isUserInAdmin = this.adminService.IsUserInAdminRole(user);
            }

            if (!isAuthenticated || !isUserInAdmin)
            {
                this.Redirect(response, "/");
            }

            this.SetupNavAndHome(true);
            this.adminService.AddGame(agbm);
            this.Redirect(response, "/admin/games");
        }

        [HttpGet]
        public IActionResult<IEnumerable<AdminGamesViewModel>> Games(
            HttpSession session, HttpResponse response)
        {
            var isAuthenticated = session.IsUserAuthenticated(this.data);
            var user = this.adminService.FindUserBySession(session);
            bool isUserInAdmin = false;
            if (user != null)
            {
                isUserInAdmin = this.adminService.IsUserInAdminRole(user);
            }

            if (!isAuthenticated || !isUserInAdmin)
            {
                this.Redirect(response, "/");
                return null;
            }

            this.SetupNavAndHome(true);
            this.adminService.SetupNavbar(user);
            return this.View(this.adminService.GetGames());
        }

        [HttpGet]
        public IActionResult<EditGameViewModel> Edit(HttpSession session, HttpResponse response, int id)
        {
            var isAuthenticated = session.IsUserAuthenticated(this.data);
            var user = this.adminService.FindUserBySession(session);
            bool isUserInAdmin = false;
            if (user != null)
            {
                isUserInAdmin = this.adminService.IsUserInAdminRole(user);
            }

            if (!isAuthenticated || !isUserInAdmin)
            {
                this.Redirect(response, "/");
                return null;
            }
            
            this.SetupNavAndHome(true);
            this.adminService.SetupNavbar(user);
            return this.View(this.adminService.GetEditGame(id));
        }

        [HttpPost]
        public void Edit(HttpResponse response, HttpSession session, EditGameBindingModel egbm)
        {
            var isAuthenticated = session.IsUserAuthenticated(this.data);
            var user = this.adminService.FindUserBySession(session);
            bool isUserInAdmin = false;
            if (user != null)
            {
                isUserInAdmin = this.adminService.IsUserInAdminRole(user);
            }

            if (!isAuthenticated || !isUserInAdmin)
            {
                this.Redirect(response, "/");
            }

            this.SetupNavAndHome(true);
            this.adminService.EditGame(egbm);
            this.Redirect(response, "/admin/games");
        }

        [HttpGet]
        public IActionResult<DeleteGameViewModel> Delete(HttpSession session, HttpResponse response, int id)
        {
            var isAuthenticated = session.IsUserAuthenticated(this.data);
            var user = this.adminService.FindUserBySession(session);
            bool isUserInAdmin = false;
            if (user != null)
            {
                isUserInAdmin = this.adminService.IsUserInAdminRole(user);
            }

            if (!isAuthenticated || !isUserInAdmin)
            {
                this.Redirect(response, "/");
                return null;
            }

            this.SetupNavAndHome(true);
            this.adminService.SetupNavbar(user);
            return this.View(this.adminService.GetDeleteGame(id));
        }

        [HttpPost]
        public void Delete(HttpResponse response, HttpSession session, int id)
        {
            var isAuthenticated = session.IsUserAuthenticated(this.data);
            var user = this.adminService.FindUserBySession(session);
            bool isUserInAdmin = false;
            if (user != null)
            {
                isUserInAdmin = this.adminService.IsUserInAdminRole(user);
            }

            if (!isAuthenticated || !isUserInAdmin)
            {
                this.Redirect(response, "/");
            }

            this.SetupNavAndHome(true);
            this.adminService.SetupNavbar(user);
            this.adminService.DeleteGame(id);
            this.Redirect(response, "/admin/games");
        }
    }
}