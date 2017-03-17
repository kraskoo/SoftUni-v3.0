namespace SimpleMVC.Extensions
{
    public static class StringExtentions
    {
        public static string ToUpperFirst(this string str) =>
            $"{str[0].ToString().ToUpper()}{str.Substring(1)}";
    }
}