namespace P11Palindromes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Palindromes
    {
        public static void Main()
        {
            string[] inputData =
                Console.ReadLine()
                .Split(new[] { ' ', '.', ',', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            SortedSet<string> palindromes = new SortedSet<string>(inputData.Where(IsPalindrome));
            Console.WriteLine("[{0}]", string.Join(", ", palindromes));
        }

        private static bool IsPalindrome(string word)
        {
            string reversedWord = word.ReverseString();
            if (reversedWord.Equals(word))
            {
                return true;
            }

            return false;
        }

        private static string ReverseString(this string @string)
        {
            return string.Join("", @string.ToCharArray().Reverse());
        }
    }
}