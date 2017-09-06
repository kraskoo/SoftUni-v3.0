namespace BuhtigIssueTracker.UI
{
    using System;
    using Interfaces;

    public class ConsoleWriter : IOutputWriter
    {
        public void WriteString(string message)
        {
            Console.WriteLine(message);
        }
    }
}