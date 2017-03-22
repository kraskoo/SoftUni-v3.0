namespace CarDealer.Data.Models
{
    using System.Data.Entity;
    using CarDealer.Models;
    using Interfaces;

    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DbContext context) : base(context)
        {
        }
    }
}