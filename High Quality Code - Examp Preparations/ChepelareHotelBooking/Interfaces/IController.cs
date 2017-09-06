namespace HotelBookingSystem.Interfaces
{
    public interface IController
    {
        string MethodName { get; }

        IUser CurrentUser { get; }

        bool HasCurrentUser { get; }
    }
}