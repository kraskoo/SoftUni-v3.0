namespace BuhtigIssueTracker.Utilities
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public static class Extensions
    {
        public static string GetHashedPassword(this string password)
        {
            return string.Join(
                string.Empty,
                SHA1.Create()
                .ComputeHash(
                    Encoding.Default.GetBytes(password))
                .Select(x => x.ToString()));
        }

        public static string GetFormattedString(this string format, params object[] arguments)
        {
            return string.Format(format, arguments);
        }

        public static T GetStringValueAsEnumType<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}