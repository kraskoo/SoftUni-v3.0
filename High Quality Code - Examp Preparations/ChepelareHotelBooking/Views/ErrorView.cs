namespace HotelBookingSystem.Views
{
    using System.Text;

    public class ErrorView : View
    {
        public ErrorView(string message) : base(message)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            var message = this.Model as string;
            viewResult
                .AppendLine("Something happened!!1")
                .AppendLine(message);
        }
    }
}