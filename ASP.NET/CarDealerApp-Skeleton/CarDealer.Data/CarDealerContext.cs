namespace CarDealer.Data
{
    using System.Data.Entity;
    using CarDealer.Models;

    public class CarDealerContext : DbContext
    {
        private const string DefaultContextName = "CarDealerContext";

        public CarDealerContext() : base(DefaultContextName)
        {
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<Part> Parts { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }
    }
}