namespace HotelBookingSystem.Interfaces
{
    using System.Collections.Generic;

    public interface IRoom : IDataEntity
    {
        int Places { get; }

        decimal PricePerDay { get; }

        ICollection<IAvailableDate> AvailableDates { get; }

        ICollection<IBooking> Bookings { get; }
    }
}