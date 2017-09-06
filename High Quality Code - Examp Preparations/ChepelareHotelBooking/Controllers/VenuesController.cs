namespace HotelBookingSystem.Controllers
{
    using Enums;
    using Interfaces;
    using Models;

    public class VenuesController : Controller
    {
        public VenuesController(
            IHotelBookingSystemData data,
            IUser user,
            string methodName) : base(data, user, methodName)
        {
        }

        public IView All()
        {
            var venues = this.Data.RepositoryWithVenues.GetAll();
            return this.View(venues);
        }

        public IView Details(int id)
        {
            this.Authorize(Role.User, Role.VenueAdmin);
            var venue = this.Data.RepositoryWithVenues.Get(id);
            if (venue == null)
            {
                return this.NotFound($"The venue with ID {id} does not exist.");
            }

            return this.View(venue);
        }

        public IView Rooms(int id)
        {
            var venue = this.Data.RepositoryWithVenues.Get(id);
            if (venue == null)
            {
                return this.NotFound($"The venue with ID {id} does not exist.");
            }

            return this.View(venue);
        }

        public IView Add(string name, string address, string description)
        {
            var newVenue = new Venue(name, address, description, CurrentUser);
            this.Data.RepositoryWithVenues.Add(newVenue);
            return this.View(newVenue);
        }
    }
}