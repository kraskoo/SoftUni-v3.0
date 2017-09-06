namespace BuhtigIssueTracker.Models
{
    using Interfaces;
    using Utilities;

    public class User : IUser
    {
        public User(string username, string password)
        {
            this.Username = username;
            this.HashedPassword = password.GetHashedPassword();
        }

        public string Username { get; }

        public string HashedPassword { get; }
    }
}