namespace BuhtigIssueTracker.Core
{
    using System;
    using DataProviders;
    using Interfaces;
    using UI;

    public class Engine : IEngine
    {
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;
        private readonly Dispatcher dispatcher;

        public Engine(IInputReader reader, IOutputWriter writer, Dispatcher dispatcher)
        {
            this.reader = reader;
            this.writer = writer;
            this.dispatcher = dispatcher;
        }

        public Engine()
            : this(new ConsoleReader(), new ConsoleWriter(), new Dispatcher())
        {
        }

        public void Run()
        {
            while (true)
            {
                string url = this.reader.ReadString();
                if (string.IsNullOrEmpty(url))
                {
                    break;
                }

                url = url.Trim();
                if (!string.IsNullOrEmpty(url))
                {
                    try
                    {
                        var endpoint = new Endpoint(url);
                        string viewResult = this.dispatcher.DispatchAction(endpoint);
                        this.writer.WriteString(viewResult);
                    }
                    catch (Exception ex)
                    {
                        this.writer.WriteString(ex.Message);
                    }
                }
            }
        }
    }
}