namespace Models.Interfaces
{
    using Enums;

    public interface IUser : IModel
    {
        UserRole UserRole { get; }
    }
}