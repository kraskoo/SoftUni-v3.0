namespace P07PredicateForNames
{
    using System;
    using System.Linq;

    public class PredicateForNames
    {
        public static void Main()
        {
            int length = int.Parse(Console.ReadLine());
            Predicate<string> nameValidator = n => n.Length <= length;
            Console.ReadLine()?
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Where(n => nameValidator(n))
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}