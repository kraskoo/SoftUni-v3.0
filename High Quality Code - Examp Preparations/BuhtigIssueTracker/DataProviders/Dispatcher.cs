namespace BuhtigIssueTracker.DataProviders
{
    using Enums;
    using Interfaces;
    using Utilities;

    public class Dispatcher
    {
        public Dispatcher(IIssueTracker tracker)
        {
            this.Tracker = tracker;
        }

        public Dispatcher() : this(new IssueTracker())
        {
        }

        public IIssueTracker Tracker { get; }

        public string DispatchAction(IEndpoint endpoint)
        {
            switch (endpoint.ActionName)
            {
                case "RegisterUser":
                    return this.Tracker
                        .RegisterUser(
                            endpoint.Parameters["username"],
                            endpoint.Parameters["password"],
                            endpoint.Parameters["confirmPassword"]);
                case "LoginUser":
                    return this.Tracker
                        .LoginUser(
                            endpoint.Parameters["username"],
                            endpoint.Parameters["password"]);
                case "LogoutUser":
                    return this.Tracker.LogoutUser();
                case "CreateIssue":
                    return this.Tracker
                        .CreateIssue(
                            endpoint.Parameters["title"],
                            endpoint.Parameters["description"],
                            endpoint.Parameters["priority"].GetStringValueAsEnumType<IssuePriority>(),
                            endpoint.Parameters["tags"].Split('|'));
                case "RemoveIssue":
                    return this.Tracker.RemoveIssue(int.Parse(endpoint.Parameters["id"]));
                case "AddComment":
                    return this.Tracker.AddComment(
                        int.Parse(endpoint.Parameters["id"]),
                        endpoint.Parameters["text"]);
                case "MyIssues":
                    return this.Tracker.GetMyIssues();
                case "MyComments":
                    return this.Tracker.GetMyComments();
                case "Search":
                    return this.Tracker.SearchForIssues(endpoint.Parameters["tags"].Split('|'));
                default:
                    return $"Invalid action: {endpoint.Parameters}";
            }
        }
    }
}