namespace Data
{
    using System.Data.Entity;
    using Models;

    public class SoftUniStoreContext : DbContext
    {
        private const string DefaultContextName = "SoftUniStoreContext";

        public SoftUniStoreContext() : base(DefaultContextName)
        {
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Login> Logins { get; set; }
    }
}