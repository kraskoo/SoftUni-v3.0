namespace Data.Interfaces
{
    public interface IDataProvidable
    {
        IUserRepository Users { get; }

        IGameRepository Games { get; }

        ILoginRepository Logins { get; }

        int SaveChanges();
    }
}