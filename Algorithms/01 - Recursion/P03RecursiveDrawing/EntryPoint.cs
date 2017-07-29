namespace P03RecursiveDrawing
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            int n = 6;//int.Parse(Console.ReadLine());
            DrawFigure(n);
        }

        private static void DrawFigure(int num)
        {
            if (num == 0)
            {
                return;
            }

            Console.WriteLine(new string('*', num));
            DrawFigure(num - 1);
            Console.WriteLine(new string('#', num));
        }
    }
}