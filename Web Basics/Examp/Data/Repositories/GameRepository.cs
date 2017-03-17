namespace Data.Repositories
{
    using System.Data.Entity;
    using Interfaces;
    using Models;

    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(DbContext context) : base(context)
        {
        }
    }
}