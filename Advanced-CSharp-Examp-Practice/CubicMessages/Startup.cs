namespace CubicMessages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class Startup
    {
        private static readonly Regex Regex = new Regex(@"^(?!\D)(\d+)([a-zA-Z]+)([^a-zA-Z]*)$");
        private static readonly Func<int, int, bool> LengthChecker = (a, b) => a > -1 && a < b;

        public static void Main()
        {
            List<string> outputList = new List<string>();
            string input = Console.ReadLine();
            while (!input.Equals("Over!"))
            {
                var matches = Regex.Matches(input);
                string message = string.Empty;
                int[] indices = null;
                foreach (Match match in matches)
                {
                    message = match.Groups[2].Value;
                    var secondMatches =
                        match
                        .Groups[3]
                        .Value
                        .Where(char.IsDigit)
                        .Select(c => int.Parse(c.ToString()));
                    indices =
                        match
                            .Groups[1]
                            .Value
                            .Select(c => int.Parse(c.ToString()))
                        .Concat(secondMatches.Any() ? secondMatches : new int[0])
                        .ToArray();
                }

                if (!string.IsNullOrEmpty(message))
                {
                    CollectMessage(message, indices, outputList);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(string.Join("\n", outputList));
        }

        private static void CollectMessage(string message, int[] indices, List<string> outputList)
        {
            int lenghtOfMessage = int.Parse(Console.ReadLine());
            if (lenghtOfMessage == message.Length)
            {
                char[] encoded = new char[indices.Length];
                for (int i = 0; i < indices.Length; i++)
                {
                    if (LengthChecker(indices[i], message.Length))
                    {
                        encoded[i] = message[indices[i]];
                    }
                    else
                    {
                        encoded[i] = ' ';
                    }
                }

                outputList.Add($"{message} == {string.Join("", encoded)}");
            }
        }
    }
}