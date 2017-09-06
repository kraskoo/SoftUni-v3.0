namespace BuhtigIssueTracker.UI
{
    using System;
    using Interfaces;

    public class ConsoleReader : IInputReader
    {
        public string ReadString()
        {
            return Console.ReadLine();
        }
    }
}