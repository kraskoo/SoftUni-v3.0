namespace HotelBookingSystem.Interfaces
{
    using System.Collections.Generic;
    using Enums;

    public interface IUser : IDataEntity
    {
        string Username { get; }

        string PasswordHash { get; }

        Role Role { get; }

        ICollection<IBooking> Bookings { get; }
    }
}