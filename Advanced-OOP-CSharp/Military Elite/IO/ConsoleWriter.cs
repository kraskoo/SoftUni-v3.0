namespace P08MilitaryElite.IO
{
    using System;
    using Interfaces;

    public class ConsoleWriter : IOutputWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}