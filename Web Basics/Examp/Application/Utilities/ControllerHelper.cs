namespace Application.Utilities
{
    using Common;
    using Common.Utilities;
    using Data.Services;
    using SimpleMVC.Controllers;
    using User = Models.User;

    public static class ControllerHelper
    {
        public static void SetupNavAndHome(this Controller controller, bool isAuthenticated)
        {
            if (!isAuthenticated)
            {
                Constants.HomeStart = string.Format(Constants.HomeStartHtml.GetContentByName(), string.Empty);
                Constants.NavHtml = Constants.NavNotLoggedHtml.GetContentByName();
                return;
            }

            var homeStartLinkHtml =
                string.Format(Constants.HomeStartLinksHtml.GetContentByName(), "Owned");
            Constants.HomeStart = Constants.HomeStartHtml.GetContentByName().GetHomeStart(homeStartLinkHtml);
        }

        public static void SetGameDetails(this Controller controller, Service service, User user, int gameId)
        {
            Constants.GameDetailsForm = user == null ||
                service.CheckIfUserIsBuyedGame(user.Id, gameId) ?
                    string.Empty :
                    string.Format(Constants.GameDetailsFormHtml.GetContentByName(), user.Id, gameId);
        }

        public static void SetupNavbar(this Service service, User user)
        {
            Constants.NavHtml = string.Format(
                    service.IsUserInAdminRole(user) ?
                    Constants.NavLoggedAdminHtml.GetContentByName() :
                    Constants.NavLoggedHtml.GetContentByName(),
                    user.FullName);
        }
    }
}