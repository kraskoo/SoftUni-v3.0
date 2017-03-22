namespace CarDealer.Data
{
    using System.Data.Entity;
    using Interfaces;
    using Models;

    public class CarDealerData : IDataProvidable
    {
        private readonly DbContext context;

        public CarDealerData(DbContext context)
        {
            this.context = context;
        }

        public ICarRepository Cars => new CarRepository(this.context);

        public ICustomerRepository Customers => new CustomerRepository(this.context);

        public IPartRepository Parts => new PartRepository(this.context);

        public ISaleRepository Sales => new SaleRepository(this.context);

        public ISupplierRepository Suppliers => new SupplierRepository(this.context);

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}