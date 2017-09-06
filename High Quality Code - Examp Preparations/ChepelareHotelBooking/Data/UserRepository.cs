namespace HotelBookingSystem.Data
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class UserRepository : Repository<IUser>, IUserRepository
    {
        private readonly Dictionary<string, IUser> usersByUsername;

        public UserRepository()
        {
            this.usersByUsername = new Dictionary<string, IUser>();
        }

        public IUser GetByUsername(string username)
        {
            if (!this.usersByUsername.ContainsKey(username))
            {
                return null;
            }

            return this.usersByUsername[username];
        }

        public override void Add(IUser user)
        {
            this.usersByUsername.Add(user.Username, user);
            base.Add(user);
        }

        public override bool Update(int id, IUser newUser)
        {
            var user = this.Get(id);
            if (user.Username != newUser.Username)
            {
                throw new InvalidOperationException("A user's username cannot be changed.");
            }

            if (base.Update(id, newUser))
            {
                this.usersByUsername[newUser.Username] = newUser;
                return true;
            }

            return false;
        }

        public override bool Delete(int id)
        {
            var user = this.Get(id);
            this.usersByUsername.Remove(user.Username);
            return base.Delete(id);
        }
    }
}