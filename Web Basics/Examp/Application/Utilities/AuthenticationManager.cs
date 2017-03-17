namespace Application.Utilities
{
    using System.Linq;
    using Data.Interfaces;
    using Models;
    using SimpleHttpServer.Models;
    using SimpleHttpServer.Utilities;

    public static class AuthenticationManager
    {
        public static bool IsUserAuthenticated(this HttpSession session, IDataProvidable data)
        {
            return data.Logins.GetAll().Any(login => login.SessionId == session.Id && login.IsActive);
        }

        public static User GetAuthenticatedUser(this HttpSession session, IDataProvidable data)
        {
            var login = data.Logins.Find(l => l.SessionId == session.Id && l.IsActive);
            return login?.User;
        }

        public static void Logout(this HttpSession session, IDataProvidable data, HttpResponse response)
        {
            var login = data.Logins.Find(l => l.SessionId == session.Id && l.IsActive);
            login.IsActive = false;
            data.SaveChanges();
            var newSession = SessionCreator.Create();
            var sessionCookie = new Cookie("sessionId", $"{newSession.Id}; HttpOnly; path=/");
            response.Header.AddCookie(sessionCookie);
        }
    }
}