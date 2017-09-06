namespace HotelBookingSystem.Views.Rooms
{
    using System.Text;
    using Interfaces;

    public class Add : View
    {
        public Add(IRoom room) : base(room)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var room = this.Model as IRoom;
            viewResult.AppendFormat(
                    "The room with ID {0} has been created successfully.",
                    room.Id)
                .AppendLine();
        }
    }
}