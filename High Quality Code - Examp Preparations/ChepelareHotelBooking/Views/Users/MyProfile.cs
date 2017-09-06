namespace HotelBookingSystem.Views.Users
{
    using System.Linq;
    using System.Text;
    using Interfaces;

    public class MyProfile : View
    {
        public MyProfile(IUser user) : base(user)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var user = this.Model as IUser;
            viewResult.AppendLine(user.Username);
            if (!user.Bookings.Any())
            {
                viewResult.AppendLine("You have not made any bookings yet.");
            }
            else
            {
                viewResult.AppendLine("Your bookings:");
                foreach (var booking in user.Bookings)
                {
                    viewResult
                        .AppendFormat(
                            "* {0:dd.MM.yyyy} - {1:dd.MM.yyyy} (${2:F2})",
                            booking.StartDate,
                            booking.EndDate,
                            booking.TotalPrice)
                        .AppendLine();
                }
            }
        }
    }
}