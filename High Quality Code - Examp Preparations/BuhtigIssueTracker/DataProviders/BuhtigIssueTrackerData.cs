namespace BuhtigIssueTracker.DataProviders
{
    using System.Collections.Generic;
    using Interfaces;
    using Wintellect.PowerCollections;

    public class BuhtigIssueTrackerData : IBuhtigIssueTrackerData
    {
        private int nextIssueId;

        public BuhtigIssueTrackerData()
        {
            this.UsersByName = new Dictionary<string, IUser>();
            this.IssuesById = new OrderedDictionary<int, IIssue>();
            this.IssuesByUser = new MultiDictionary<string, IIssue>(true);
            this.IssuesByTag = new MultiDictionary<string, IIssue>(true);
            this.CommentsByUser = new MultiDictionary<IUser, IComment>(true);
            this.nextIssueId = 0;
        }

        public IUser CurrentlyLoggedIn { get; set; }

        public IDictionary<string, IUser> UsersByName { get; }

        public OrderedDictionary<int, IIssue> IssuesById { get; }

        public MultiDictionary<string, IIssue> IssuesByUser { get; }

        public MultiDictionary<string, IIssue> IssuesByTag { get; }

        public MultiDictionary<IUser, IComment> CommentsByUser { get; }

        public int AddIssue(IIssue issue)
        {
            var currentId = this.GetNextId();
            this.IssuesById.Add(currentId, issue);
            this.IssuesByUser[this.CurrentlyLoggedIn.Username].Add(issue);
            foreach (var tag in issue.Tags)
            {
                this.IssuesByTag[tag].Add(issue);
            }

            return currentId;
        }

        public void RemoveIssue(IIssue issue, int id)
        {
            this.IssuesByUser[this.CurrentlyLoggedIn.Username].Remove(issue);
            foreach (var tag in issue.Tags)
            {
                this.IssuesByTag[tag].Remove(issue);
            }

            this.IssuesById.Remove(id);
        }

        private int GetNextId()
        {
            return ++this.nextIssueId;
        }
    }
}