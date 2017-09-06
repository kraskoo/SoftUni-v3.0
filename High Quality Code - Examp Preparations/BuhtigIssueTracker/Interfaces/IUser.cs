namespace BuhtigIssueTracker.Interfaces
{
    public interface IUser
    {
        string Username { get; }

        string HashedPassword { get; }
    }
}