namespace P10Borders
{
    using System;
    using Properties;

    public class Borders
    {
        public static void Main()
        {
            var newHtml = Resources.HTML.Replace(
                @"<link rel=""stylesheet"" href=""styles/borders.css"" />",
                $"<style>{Resources.borders}</style>");
            Console.WriteLine(newHtml);
        }
    }
}