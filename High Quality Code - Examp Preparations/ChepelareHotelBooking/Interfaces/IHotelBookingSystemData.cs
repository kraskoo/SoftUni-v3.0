namespace HotelBookingSystem.Interfaces
{
    public interface IHotelBookingSystemData
    {
        IUserRepository RepositoryWithUsers { get; }

        IRepository<IVenue> RepositoryWithVenues { get; }

        IRepository<IRoom> RepositoryWithRooms { get; }

        IRepository<IBooking> RepositoryWithBookings { get; }
    }
}