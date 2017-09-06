namespace HotelBookingSystem.Views.Venues
{
    using System.Text;
    using Interfaces;

    public class Add : View
    {
        public Add(IVenue venue) : base(venue)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var venue = this.Model as IVenue;
            viewResult.AppendFormat(
                    "The venue {0} with ID {1} has been created successfully.",
                    venue.Name,
                    venue.Id)
                .AppendLine();
        }
    }
}