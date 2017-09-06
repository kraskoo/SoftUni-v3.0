namespace HotelBookingSystem.Views.Users
{
    using System.Text;
    using Interfaces;

    public class Logout : View
    {
        public Logout(IUser user) : base(user)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var user = this.Model as IUser;
            viewResult
                .AppendFormat(
                    "The user {0} has logged out.",
                    user.Username)
                .AppendLine();
        }
    }
}