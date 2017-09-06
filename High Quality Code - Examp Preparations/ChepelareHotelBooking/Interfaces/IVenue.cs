namespace HotelBookingSystem.Interfaces
{
    using System.Collections.Generic;

    public interface IVenue : IDataEntity
    {
        string Name { get; }

        string Address { get; }

        string Description { get; }

        IUser Owner { get; }

        ICollection<IRoom> Rooms { get; }
    }
}