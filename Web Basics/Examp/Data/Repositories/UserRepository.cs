namespace Data.Repositories
{
    using System.Data.Entity;
    using Interfaces;
    using Models;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}