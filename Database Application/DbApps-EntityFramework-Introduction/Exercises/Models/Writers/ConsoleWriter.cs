namespace Exercises.Models.Writers
{
    using System;
    using Interfaces;

    public class ConsoleWriter : IOutputWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }
    }
}