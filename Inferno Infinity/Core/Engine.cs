namespace P10InfernoInfinity.Core
{
    using System;
    using Interfaces;
    using IO;

    public class Engine : IRunnable
    {
        private readonly IWeaponRepository repository;
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;

        public Engine(
            IWeaponRepository repository, IInputReader reader, IOutputWriter writer)
        {
            this.repository = repository;
            this.reader = reader;
            this.writer = writer;
        }

        public Engine() : this(
            new WeaponRepository(), new ConsoleReader(), new ConsoleWriter())
        {
        }

        public void Run()
        {
            string inputLine = this.reader.ReadLine();
            while (!inputLine.Equals("END"))
            {
                string[] commandData =
                    inputLine.Split(';');
                try
                {
                    switch (commandData[0])
                    {
                        case "Create":
                            this.repository.CreateWeapon(commandData[1], commandData[2]);
                            break;
                        case "Add":
                            this.repository
                                .AddGemToWeapon(
                                    commandData[1],
                                    int.Parse(commandData[2]),
                                    commandData[3]
                                );
                            break;
                        case "Remove":
                            this.repository
                                .RemoveGemFromWeapon(
                                    commandData[1],
                                    int.Parse(commandData[2])
                                );
                            break;
                        case "Print":
                            this.writer
                                .WriteLine(
                                    this.repository
                                    .Print(
                                        commandData[1])
                                    );
                            break;
                        default:
                            throw new ArgumentException("Unknown command");
                    }
                }
                catch (ArgumentException ae)
                {
                    this.writer.WriteLine(ae.Message);
                }

                inputLine = this.reader.ReadLine();
            }
        }
    }
}