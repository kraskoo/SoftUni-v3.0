namespace BuhtigIssueTracker.Interfaces
{
    public interface IComment
    {
        IUser Author { get; }

        string Text { get; }
    }
}