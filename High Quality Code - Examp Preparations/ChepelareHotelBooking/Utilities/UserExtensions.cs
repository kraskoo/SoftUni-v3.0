namespace HotelBookingSystem.Utilities
{
    using Enums;
    using Interfaces;

    public static class UserExtensions
    {
        public static bool IsInRole(this IUser user, Role role)
        {
            return user.Role == role;
        }
    }
}