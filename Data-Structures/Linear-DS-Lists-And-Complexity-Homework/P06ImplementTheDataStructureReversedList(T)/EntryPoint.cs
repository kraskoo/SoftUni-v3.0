namespace P06ImplementTheDataStructureReversedList_T_
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            ReversedList<int> reversedInts = new ReversedList<int>();
            reversedInts.Add(3);
            reversedInts.Add(-34532);
            reversedInts.Add(78);
            reversedInts.Add(111);
            PrintReversedList(reversedInts);
            Console.WriteLine();
            reversedInts.RemoveAt(2);
            PrintReversedList(reversedInts);
        }

        private static void PrintReversedList(ReversedList<int> reversedInts)
        {
            for (int i = 0; i < reversedInts.Count; i++)
            {
                Console.WriteLine(reversedInts[i]);
            }
        }
    }
}