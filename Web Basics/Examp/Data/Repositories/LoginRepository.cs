namespace Data.Repositories
{
    using System.Data.Entity;
    using Interfaces;
    using Models;

    public class LoginRepository : Repository<Login>, ILoginRepository
    {
        public LoginRepository(DbContext context) : base(context)
        {
        }
    }
}