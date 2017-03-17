namespace Data.Services
{
    using System.Linq;
    using Models;
    using Models.Enums;
    using Interfaces;
    using SimpleHttpServer.Models;

    public abstract class Service
    {
        protected Service(IDataProvidable data)
        {
            this.Data = data;
        }

        protected IDataProvidable Data { get; }

        public User FindUserBySession(HttpSession session)
        {
            return this.Data.Logins.Find(l => l.SessionId == session.Id && l.IsActive)?.User;
        }

        public bool CheckIfUserIsBuyedGame(int userId, int gameId)
        {
            return this.Data.Users.Find(userId).UserGames.FirstOrDefault(ug => ug.Id == gameId) != null;
        }

        public bool IsUserInAdminRole(User user)
        {
            return user.UserRole == UserRole.Administrator;
        }
    }
}