namespace HotelBookingSystem.Controllers
{
    using System;
    using System.Linq;
    using Enums;
    using Interfaces;
    using Models;

    public class RoomsController : Controller
    {
        public RoomsController(
            IHotelBookingSystemData data,
            IUser user,
            string methodName) : base(data, user, methodName)
        {
        }

        public IView Add(int venueId, int places, decimal pricePerDay)
        {
            this.Authorize(Role.VenueAdmin);
            var venue = this.Data.RepositoryWithVenues.Get(venueId);
            if (venue == null)
            {
                return this.NotFound($"The venue with ID {venueId} does not exist.");
            }

            var newRoom = new Room(places, pricePerDay);
            venue.Rooms.Add(newRoom);
            this.Data.RepositoryWithRooms.Add(newRoom);
            return this.View(newRoom);
        }

        public IView AddPeriod(int roomId, DateTime startDate, DateTime endDate)
        {
            this.Authorize(Role.VenueAdmin);
            var room = this.Data.RepositoryWithRooms.Get(roomId);
            if (room == null)
            {
                return this.NotFound($"The room with ID {roomId} does not exist.");
            }

            room.AvailableDates.Add(new AvailableDate(startDate, endDate));
            return this.View(room);
        }

        public IView ViewBookings(int id)
        {
            this.Authorize(Role.VenueAdmin);
            var room = Data.RepositoryWithRooms.Get(id);
            if (room == null)
            {
                return this.NotFound($"The room with ID {id} does not exist.");
            }

            return this.View(room.Bookings);
        }

        public IView Book(int roomId, DateTime startDate, DateTime endDate, string comments)
        {
            this.Authorize(Role.User, Role.VenueAdmin);
            var room = this.Data.RepositoryWithRooms.Get(roomId);
            if (room == null)
            {
                return this.NotFound($"The room with ID {roomId} does not exist.");
            }

            if (endDate < startDate)
            {
                throw new ArgumentException("The date range is invalid.");
            }

            var availablePeriod = room.AvailableDates.FirstOrDefault(d => d.StartDate <= startDate || d.EndDate >= endDate);
            if (availablePeriod == null)
            {
                throw new ArgumentException(
                    $"The room is not available to book in the period {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}.");
            }

            decimal totalPrice = (endDate - startDate).Days * room.PricePerDay;
            var booking = new Booking(this.CurrentUser, startDate, endDate, totalPrice, comments);
            room.Bookings.Add(booking);
            this.CurrentUser.Bookings.Add(booking);
            this.UpdateRoomAvailability(startDate, endDate, room, availablePeriod);
            return this.View(booking);
        }

        // This works, don't touch!
        private void UpdateRoomAvailability(DateTime startDate, DateTime endDate, IRoom room, IAvailableDate availablePeriod)
        {
            room.AvailableDates.Remove(availablePeriod);
            var periodBeforeBooking = startDate - availablePeriod.StartDate;
            if (periodBeforeBooking > TimeSpan.Zero)
            {
                room.AvailableDates
                    .Add(
                        new AvailableDate(
                            availablePeriod.StartDate,
                            availablePeriod.StartDate.Add(periodBeforeBooking)));
            }

            var periodAfterBooking = availablePeriod.EndDate - endDate;
            if (periodAfterBooking > TimeSpan.Zero)
            {
                room.AvailableDates
                    .Add(
                        new AvailableDate(
                            availablePeriod.EndDate.Subtract(periodAfterBooking),
                            availablePeriod.EndDate));
            }
        }
    }
}