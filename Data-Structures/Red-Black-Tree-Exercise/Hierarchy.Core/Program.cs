namespace Hierarchy.Core
{
    using System;

    class Program
    {
        static void Main()
        {
            var hierarchy = new Hierarchy<string>("Leonidas")
            {
                {"Leonidas", "Xena The Princess Warrior"},
                {"Leonidas", "General Protos"},
                {"Xena The Princess Warrior", "Gorok"},
                {"Xena The Princess Warrior", "Bozot"},
                {"General Protos", "Subotli"},
                {"General Protos", "Kira"},
                {"General Protos", "Zaler"}
            };

            var children = hierarchy.GetChildren("Leonidas");
            Console.WriteLine(string.Join(", ", children));

            var parent = hierarchy.GetParent("Kira");
            Console.WriteLine(parent);

            hierarchy.Remove("General Protos");
            children = hierarchy.GetChildren("Leonidas");
            Console.WriteLine(string.Join(", ", children));

            foreach (var item in hierarchy)
            {
                Console.WriteLine(item);
            }
        }
    }
}
