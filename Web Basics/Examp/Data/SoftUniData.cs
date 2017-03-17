namespace Data
{
    using Interfaces;
    using Repositories;

    public class SoftUniData : IDataProvidable
    {
        private readonly SoftUniStoreContext context;

        public SoftUniData(SoftUniStoreContext context)
        {
            this.context = context;
        }

        public IUserRepository Users => new UserRepository(this.context);

        public IGameRepository Games => new GameRepository(this.context);

        public ILoginRepository Logins => new LoginRepository(this.context);

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}