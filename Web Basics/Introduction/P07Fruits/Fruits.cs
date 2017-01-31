namespace P07Fruits
{
    using System;
    using Properties;

    public class Fruits
    {
        public static void Main()
        {
            var newHtml = Resources.HTML.Replace("images/apple.png", Resources.AppleBase64);
            newHtml = newHtml.Replace("images/banana.png", Resources.BananaBase64);
            newHtml = newHtml.Replace("images/kiwi.png", Resources.KiwiBase64);
            newHtml = newHtml.Replace("images/orange.png", Resources.OrangeBase64);
            Console.WriteLine(newHtml);
        }
    }
}