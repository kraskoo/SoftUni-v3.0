namespace HotelBookingSystem.Views.Rooms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interfaces;

    public class ViewBookings : View
    {
        public ViewBookings(IEnumerable<IBooking> bookings) : base(bookings)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var bookings = this.Model as IEnumerable<IBooking>;
            if (!bookings.Any())
            {
                viewResult.AppendLine("There are no bookings for this room.");
            }
            else
            {
                viewResult.AppendLine("Room bookings:");
                foreach (var booking in bookings)
                {
                    viewResult.AppendFormat(
                            "* {0:dd.MM.yyyy} - {1:dd.MM.yyyy} (${2:F2})",
                            booking.StartDate,
                            booking.EndDate,
                            booking.TotalPrice)
                        .AppendLine();
                }

                viewResult.AppendFormat(
                        "Total booking price: ${0:F2}",
                        bookings.Sum(b => b.TotalPrice))
                    .AppendLine();
            }            
        }
    }
}