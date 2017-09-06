namespace HotelBookingSystem.Views.Users
{
    using System.Text;
    using Interfaces;

    public class Register : View
    {
        public Register(IUser user) : base(user)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var user = this.Model as IUser;
            viewResult
                .AppendFormat("The user {0} has been registered and may login.", user.Username)
                .AppendLine();
        }
    }
}