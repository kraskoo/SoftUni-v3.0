namespace CarDealer.Data.Models
{
    using System.Data.Entity;
    using CarDealer.Models;
    using Interfaces;

    public class PartRepository : Repository<Part>, IPartRepository
    {
        public PartRepository(DbContext context) : base(context)
        {
        }
    }
}