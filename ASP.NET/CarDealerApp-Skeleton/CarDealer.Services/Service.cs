namespace CarDealer.Services
{
    using Data.Interfaces;

    public abstract class Service
    {
        protected Service(IDataProvidable data)
        {
            this.Data = data;
        }

        protected IDataProvidable Data { get; }
    }
}