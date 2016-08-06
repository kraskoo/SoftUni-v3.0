namespace P08MilitaryElite.Handlers
{
    using Interfaces;
    using IO;

    public class InputHandler : IInputHandler
    {
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;

        public InputHandler(IInputReader reader, IOutputWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public InputHandler()
            : this(new ConsoleReader(), new ConsoleWriter())
        {
        }

        public string ReadLine()
        {
            return this.reader.ReadLine();
        }

        public void WriteLine(string message)
        {
            this.writer.WriteLine(message);
        }

        public void WriteLine(object obj)
        {
            this.writer.WriteLine(obj);
        }
    }
}