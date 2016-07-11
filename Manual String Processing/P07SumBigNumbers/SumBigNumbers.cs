namespace P07SumBigNumbers
{
    using System;
    using System.Collections.Generic;

    public class SumBigNumbers
    {
        public static void Main()
        {
            string firstNum = Console.ReadLine();
            string secondNum = Console.ReadLine();
            Console.WriteLine(SumOfBigIntegers(firstNum, secondNum));
        }

        public static string SumOfBigIntegers(string firstNum, string secondNum)
        {
            TrimStrings(ref firstNum, ref secondNum);
            LinkedList<string> newNum = new LinkedList<string>();
            int reminder = 0;
            for (int i = firstNum.Length - 1; i >= 0; i--)
            {
                int sum = int.Parse(firstNum[i].ToString()) + int.Parse(secondNum[i].ToString()) + reminder;
                int lastDigit = i > 0 ? (sum < 10 ? sum : sum % 10) : sum;
                newNum.AddFirst(lastDigit.ToString());
                reminder = sum < 10 ? 0 : 1;
            }

            return string.Join("", newNum);
        }

        public static void TrimStrings(ref string firstNum, ref string secondNum)
        {
            EnsureOfStrings(ref firstNum, ref secondNum);
            if (firstNum.Length < secondNum.Length)
            {
                firstNum = firstNum.PadLeft(secondNum.Length, '0');
            }
            else if (secondNum.Length < firstNum.Length)
            {
                secondNum = secondNum.PadLeft(firstNum.Length, '0');
            }
        }

        public static void EnsureOfStrings(ref string firstNum, ref string secondNum)
        {
            firstNum = firstNum.TrimStart('0');
            secondNum = secondNum.TrimStart('0');
        }
    }
}