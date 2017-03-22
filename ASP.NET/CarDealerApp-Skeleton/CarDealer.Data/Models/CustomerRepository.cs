namespace CarDealer.Data.Models
{
    using System.Data.Entity;
    using CarDealer.Models;
    using Interfaces;

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }
    }
}