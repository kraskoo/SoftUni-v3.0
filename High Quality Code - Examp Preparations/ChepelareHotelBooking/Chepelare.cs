namespace HotelBookingSystem
{
    using System.Globalization;
    using System.Threading;
    using Core;
    using Interfaces;
    using UI;

    public class HotelBookingSystemMain
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            IInputReader reader = new ConsoleReader();
            IOutputWriter writer = new ConsoleWriter();
            var engine = new Engine(reader, writer);
            engine.StartOperation();
        }
    }
}