namespace HotelBookingSystem.Views.Rooms
{
    using System.Text;
    using Interfaces;

    public class Book : View
    {
        public Book(IBooking booking) : base(booking)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var booking = this.Model as IBooking;
            viewResult
                .AppendFormat(
                    "Room booked from {0:dd.MM.yyyy} to {1:dd.MM.yyyy} for ${2:F2}.",
                    booking.StartDate,
                    booking.EndDate,
                    booking.TotalPrice)
                .AppendLine();
        }
    }
}