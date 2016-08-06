namespace P08CustomComparator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CustomComparator
    {
        private class NewComparer : IComparer<int>
        {
            private readonly Predicate<int> valueValidator = x => x % 2 == 0;

            public int Compare(int x, int y)
            {
                if ((this.valueValidator(x) && this.valueValidator(y)) ||
                    (!this.valueValidator(x) && !this.valueValidator(y)))
                {
                    return x.CompareTo(y);
                }

                if (this.valueValidator(x))
                {
                    return -1;
                }

                return 1;
            }
        }

        public static void Main()
        {
            int[] numbers =
                Console.ReadLine()?
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Array.Sort(numbers, new NewComparer());
            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}