namespace HotelBookingSystem.Views.Rooms
{
    using System.Text;
    using Interfaces;

    public class AddPeriod : View
    {
        public AddPeriod(IRoom room) : base(room)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var room = this.Model as IRoom;
            viewResult.AppendFormat(
                    "The period has been added to room with ID {0}.",
                    room.Id)
                .AppendLine();
        }
    }
}