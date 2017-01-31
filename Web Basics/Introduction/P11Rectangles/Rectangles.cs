namespace P11Rectangles
{
    using System;
    using Properties;

    public class Rectangles
    {
        public static void Main()
        {
            var newHtml = Resources.HTML.Replace(
                "<link rel=\"stylesheet\" href=\"styles/rectangles.css\" />",
                $"<style>{Resources.rectangles}</style>");
            Console.WriteLine(newHtml);
        }
    }
}