namespace HotelBookingSystem.Interfaces
{
    using System;

    public interface IAvailableDate
    {
        DateTime StartDate { get; }

        DateTime EndDate { get; }
    }
}