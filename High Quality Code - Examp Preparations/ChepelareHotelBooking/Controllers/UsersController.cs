namespace HotelBookingSystem.Controllers
{
    using System;
    using Enums;
    using Interfaces;
    using Models;
    using Utilities;

    public class UsersController : Controller
    {
        public UsersController(
            IHotelBookingSystemData data,
            IUser user,
            string methodName) : base(data, user, methodName)
        {
        }

        public IView Register(string username, string password, string confirmPassword, string role)
        {
            if (password != confirmPassword)
            {
                throw new ArgumentException("The provided passwords do not match.");
            }

            this.EnsureNoLoggedInUser();

            var existingUser = this.Data.RepositoryWithUsers.GetByUsername(username);
            if (existingUser != null)
            {
                throw new ArgumentException($"A user with username {username} already exists.");
            }

            var userRole = (Role)Enum.Parse(typeof(Role), role, true);
            var user = new User(username, password, userRole);
            this.Data.RepositoryWithUsers.Add(user);
            return this.View(user);
        }

        public IView Login(string username, string password)
        {
            this.EnsureNoLoggedInUser();

            var existingUser = this.Data.RepositoryWithUsers.GetByUsername(username);
            if (existingUser == null)
            {
                throw new ArgumentException($"A user with username {username} does not exist.");
            }

            if (existingUser.PasswordHash != password.GetSha256Hash())
            {
                throw new ArgumentException("The provided password is wrong.");
            }

            this.CurrentUser = existingUser;
            return this.View(existingUser);
        }

        public IView MyProfile()
        {
            this.Authorize(Role.User, Role.VenueAdmin);
            return this.View(this.CurrentUser);
        }

        public IView Logout()
        {
            this.Authorize(Role.User, Role.VenueAdmin);

            var user = this.CurrentUser;
            this.CurrentUser = null;
            return this.View(user);
        }

        private void EnsureNoLoggedInUser()
        {
            foreach (var user in this.Data.RepositoryWithUsers.GetAll())
            {
                if (string.IsNullOrEmpty(user.Username) ||
                    (this.CurrentUser != null && this.CurrentUser.Username == user.Username))
                {
                    throw new ArgumentException("There is already a logged in user.");
                }
            }
        }
    }
}