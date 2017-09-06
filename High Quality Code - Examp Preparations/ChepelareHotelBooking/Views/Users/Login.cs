namespace HotelBookingSystem.Views.Users
{
    using System.Text;
    using Interfaces;

    public class Login : View
    {
        public Login(IUser user) : base(user)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var user = this.Model as IUser;
            viewResult.AppendFormat(
                    "The user {0} has logged in.",
                    user.Username)
                .AppendLine();
        }
    }
}