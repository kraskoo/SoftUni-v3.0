namespace BuhtigIssueTracker
{
    using System.Globalization;
    using System.Threading;
    using Core;
    using Interfaces;

    public class IssueTrackerApplication
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}