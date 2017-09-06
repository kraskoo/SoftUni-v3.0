namespace HotelBookingSystem.Interfaces
{
    public interface IBooking : IAvailableDate, IDataEntity
    {
        IUser Client { get; }

        decimal TotalPrice { get; }
    }
}