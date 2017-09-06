namespace BuhtigIssueTracker.Interfaces
{
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public interface IBuhtigIssueTrackerData
    {
        IUser CurrentlyLoggedIn { get; set; }

        IDictionary<string, IUser> UsersByName { get; }

        OrderedDictionary<int, IIssue> IssuesById { get; }

        MultiDictionary<string, IIssue> IssuesByUser { get; }

        MultiDictionary<string, IIssue> IssuesByTag { get; }

        MultiDictionary<IUser, IComment> CommentsByUser { get; }

        int AddIssue(IIssue issue);

        void RemoveIssue(IIssue issue, int id);
    }
}