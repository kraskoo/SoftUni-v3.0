namespace HotelBookingSystem.Data
{
    using Interfaces;

    public class HotelBookingSystemData : IHotelBookingSystemData
    {
        public HotelBookingSystemData()
        {
            this.RepositoryWithUsers = new UserRepository();
            this.RepositoryWithVenues = new Repository<IVenue>();
            this.RepositoryWithRooms = new Repository<IRoom>();
            this.RepositoryWithBookings = new Repository<IBooking>();
        }

        public IUserRepository RepositoryWithUsers { get; }

        public IRepository<IVenue> RepositoryWithVenues { get; }

        public IRepository<IRoom> RepositoryWithRooms { get; }

        public IRepository<IBooking> RepositoryWithBookings { get; }
    }
}