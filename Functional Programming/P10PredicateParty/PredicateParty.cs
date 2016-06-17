namespace P10PredicateParty
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PredicateParty
    {
        public static void Main()
        {
            List<string> partyPeople =
                Console.ReadLine()?
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            List<string> commands = new List<string>();
            string command = Console.ReadLine();
            while (!command.Equals("Party!"))
            {
                commands.Add(command);
                command = Console.ReadLine();
            }

            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].GetCommandResult(partyPeople);
            }

            if (partyPeople.Count == 0)
            {
                Console.WriteLine("Nobody is going to the party!");
            }
            else
            {
                Console.WriteLine("{0} are going to the party!", string.Join(", ", partyPeople));
            }
        }

        public static void GetCommandResult(this string commandLine, List<string> partyPeople)
        {
            string[] commandData = commandLine.Split();
            string command = commandData[0];
            string commandArg = commandData[1];
            string commandValue = commandData[2];
            switch (commandArg)
            {
                case "StartsWith":
                    if (partyPeople.Any(p => p.StartsWithChecker(commandValue)))
                    {
                        string valueOf = partyPeople.First(p => p.StartsWithChecker(commandValue));
                        ExecuteCommand(partyPeople, command, valueOf);
                    }

                    break;
                case "EndsWith":
                    if (partyPeople.Any(p => p.EndsWithChecker(commandValue)))
                    {
                        string valueOf = partyPeople.First(p => p.EndsWithChecker(commandValue));
                        ExecuteCommand(partyPeople, command, valueOf);
                    }

                    break;
                case "Length":
                    if (partyPeople.Any(p => p.LengthWithChecker(int.Parse(commandValue))))
                    {
                        ExecuteLengthCommand(partyPeople, command, int.Parse(commandValue));
                    }

                    break;
            }
        }

        private static void ExecuteLengthCommand(List<string> partyPeople, string command, int length)
        {
            switch (command)
            {
                case "Remove":
                    partyPeople.RemoveAll(p => p.Length.Equals(length));
                    break;
                case "Double":
                    List<string> alreadyAdded = new List<string>();
                    partyPeople.ForEach(p =>
                    {
                        if (p.Length.Equals(length) && !alreadyAdded.Contains(p))
                        {
                            string valueOf = partyPeople.First(peep => peep.Equals(p));
                            int indexOf = partyPeople.IndexOf(valueOf) + 1;
                            int count = partyPeople.Count(peep => peep.Equals(valueOf));
                            partyPeople.InsertRange(indexOf, Enumerable.Repeat(valueOf, count));
                            alreadyAdded.Add(p);
                        }
                    });

                    break;
            }
        }

        private static void ExecuteCommand(List<string> partyPeople, string command, string valueOf)
        {
            switch (command)
            {
                case "Remove":
                    partyPeople.RemoveAll(p => p.Equals(valueOf));
                    break;
                case "Double":
                    int indexOf = partyPeople.IndexOf(valueOf) + 1;
                    int count = partyPeople.Count(p => p.Equals(valueOf));
                    partyPeople.InsertRange(indexOf, Enumerable.Repeat(valueOf, count));
                    break;
            }
        }

        private static Func<string, bool> StartsWithFunc(string startingString)
            => n => n.StartsWith(startingString);

        private static Func<string, bool> EndsWithFunc(string endingString)
            => n => n.EndsWith(endingString);

        private static Func<string, bool> LengthWithFunc(int lenght)
            => n => n.Length.Equals(lenght);

        private static bool StartsWithChecker(this string thisValue, string invokeWith) =>
            StartsWithFunc(invokeWith).Invoke(thisValue);

        private static bool EndsWithChecker(this string thisValue, string invokeWith) =>
            EndsWithFunc(invokeWith).Invoke(thisValue);

        private static bool LengthWithChecker(this string thisValue, int length) =>
            LengthWithFunc(length).Invoke(thisValue);
    }
}