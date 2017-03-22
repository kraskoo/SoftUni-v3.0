namespace CarDealer.Data.Interfaces
{
    public interface IDataProvidable
    {
        ICarRepository Cars { get; }

        ICustomerRepository Customers { get; }

        IPartRepository Parts { get; }

        ISaleRepository Sales { get; }

        ISupplierRepository Suppliers { get; }

        int SaveChanges();
    }
}