namespace P12Languages
{
    using System;
    using Properties;

    public class Languages
    {
        public static void Main()
        {
            var newHtml = Resources.HTML.Replace(
                "<link rel=\"stylesheet\" href=\"styles/langs.css\" />",
                $"<style>{Resources.langs}</style>");
            Console.WriteLine(newHtml);
        }
    }
}