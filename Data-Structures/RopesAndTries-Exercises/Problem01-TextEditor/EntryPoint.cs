namespace Problem01_TextEditor
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            var textEditor = new TextEditor();
            ProceedSolution(textEditor);
        }

        private static void ProceedSolution(TextEditor textEditor)
        {
            while (true)
            {
                string line = Console.ReadLine();
                string[] lineArgs = line.Split();
                bool isFirstLineArgIsCommand;
                ExecuteMainCommands(textEditor, lineArgs, out isFirstLineArgIsCommand);
                ExecuteUserCommands(textEditor, lineArgs, isFirstLineArgIsCommand, line);
            }
        }

        private static void ExecuteMainCommands(
            TextEditor textEditor,
            string[] lineArgs,
            out bool isFirstLineArgIsCommand)
        {
            isFirstLineArgIsCommand = false;
            string command = lineArgs[0];
            switch (command)
            {
                case "login":
                    textEditor.Login(lineArgs[1]);
                    isFirstLineArgIsCommand = true;
                    break;
                case "logout":
                    textEditor.Logout(lineArgs[1]);
                    isFirstLineArgIsCommand = true;
                    break;
                case "users":
                    Console.WriteLine(
                        string.Join(
                            Environment.NewLine,
                            lineArgs.Length == 1 ?
                                textEditor.Users() :
                                textEditor.Users(lineArgs[1])));
                    isFirstLineArgIsCommand = true;
                    break;
                case "end":
                    Environment.Exit(0);
                    break;
            }
        }

        private static void ExecuteUserCommands(
            TextEditor textEditor,
            string[] lineArgs,
            bool isFirstLineArgIsCommand,
            string line)
        {
            if (!isFirstLineArgIsCommand)
            {
                string command = lineArgs[1];
                switch (command)
                {
                    case "insert":
                        int indexOfContinue = line.IndexOf(lineArgs[3], StringComparison.CurrentCulture);
                        textEditor.Insert(
                            lineArgs[0],
                            int.Parse(lineArgs[2]),
                            line.Substring(indexOfContinue + 1, line.Length - indexOfContinue - 2));
                        break;
                    case "prepend":
                        int prependIndexOfContinue = line.IndexOf(lineArgs[2], StringComparison.CurrentCulture);
                        textEditor.Prepend(
                            lineArgs[0],
                            line.Substring(prependIndexOfContinue + 1, line.Length - prependIndexOfContinue - 2));
                        break;
                    case "substring":
                        textEditor.Substring(lineArgs[0], int.Parse(lineArgs[2]), int.Parse(lineArgs[3]));
                        break;
                    case "delete":
                        textEditor.Delete(lineArgs[0], int.Parse(lineArgs[2]), int.Parse(lineArgs[3]));
                        break;
                    case "clear":
                        textEditor.Clear(lineArgs[0]);
                        break;
                    case "length":
                        Console.WriteLine(textEditor.Length(lineArgs[0]));
                        break;
                    case "print":
                        Console.WriteLine(textEditor.Print(lineArgs[0]));
                        break;
                    case "undo":
                        textEditor.Undo(lineArgs[0]);
                        break;
                }
            }
        }
    }
}