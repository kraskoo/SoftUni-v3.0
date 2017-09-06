namespace HotelBookingSystem.Utilities
{
    using System.Security.Cryptography;
    using System.Text;

    public static class HashUtilities
    {
        public static string GetSha256Hash(this string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var hashOutput = new StringBuilder();
            foreach (var @byte in new SHA256Managed().ComputeHash(bytes))
            {
                hashOutput.AppendFormat("{0:x2}", @byte);
            }

            return hashOutput.ToString();
        }
    }
}