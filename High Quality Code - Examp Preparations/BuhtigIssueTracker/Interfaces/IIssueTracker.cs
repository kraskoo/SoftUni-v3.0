namespace BuhtigIssueTracker.Interfaces
{
    using Enums;

    public interface IIssueTracker
    {
        string RegisterUser(string username, string password, string confirmPassword);

        string LoginUser(string username, string password);

        string LogoutUser();

        string CreateIssue(string title, string description, IssuePriority priority, string[] tags);

        string RemoveIssue(int issueId);

        string AddComment(int issueId, string text);

        string GetMyIssues();

        string GetMyComments();

        string SearchForIssues(string[] tags);
    }
}