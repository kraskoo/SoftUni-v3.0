namespace CarDealer.Data.Models
{
    using System.Data.Entity;
    using CarDealer.Models;
    using Interfaces;

    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(DbContext context) : base(context)
        {
        }
    }
}