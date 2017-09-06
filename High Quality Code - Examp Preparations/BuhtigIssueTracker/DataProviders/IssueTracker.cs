namespace BuhtigIssueTracker.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Enums;
    using Interfaces;
    using Models;
    using Utilities;

    public class IssueTracker : IIssueTracker
    {
        public IssueTracker(IBuhtigIssueTrackerData data)
        {
            this.Data = data;
        }

        public IssueTracker() : this(
            new BuhtigIssueTrackerData())
        {
        }

        public IBuhtigIssueTrackerData Data { get; }

        public string RegisterUser(
            string username,
            string password,
            string confirmPassword)
        {
            if (this.Data.CurrentlyLoggedIn != null)
            {
                return Messages.AlreadyLoggedInUser;
            }

            if (password != confirmPassword)
            {
                return Messages.ProvidedPasswordDoesntMatch;
            }

            if (this.Data.UsersByName.ContainsKey(username))
            {
                return Messages
                    .AlreadyRegistredUser
                    .GetFormattedString(username);
            }

            var user = new User(username, password);
            this.Data.UsersByName.Add(username, user);
            return Messages
                .SuccessfulRegisteredUser
                .GetFormattedString(username);
        }

        public string LoginUser(string username, string password)
        {
            if (this.Data.CurrentlyLoggedIn != null)
            {
                return Messages.AlreadyLoggedInUser;
            }

            if (!this.Data.UsersByName.ContainsKey(username))
            {
                return Messages
                    .UserIsNotRegistredAtTheSystem
                    .GetFormattedString(username);
            }

            var user = this.Data.UsersByName[username];
            if (user.HashedPassword != password.GetHashedPassword())
            {
                return Messages
                    .InvalidPassword
                    .GetFormattedString(username);
            }

            this.Data.CurrentlyLoggedIn = user;

            return Messages
                .SuccessfullyLoggedIn
                .GetFormattedString(username);
        }

        public string LogoutUser()
        {
            if (this.Data.CurrentlyLoggedIn == null)
            {
                return Messages.CurrentlyNoUserLoggedInAtSystem;
            }

            string username = this.Data.CurrentlyLoggedIn.Username;
            this.Data.CurrentlyLoggedIn = null;
            return Messages
                .SuccessfullyLogOut
                .GetFormattedString(username);
        }

        public string CreateIssue(
            string title,
            string description,
            IssuePriority priority,
            string[] strings)
        {
            if (this.Data.CurrentlyLoggedIn == null)
            {
                return Messages.CurrentlyNoUserLoggedInAtSystem;
            }

            var issue = new Issue(title, description, priority, strings.Distinct().ToList());
            int id = this.Data.AddIssue(issue);
            return Messages
                .SuccessfullyIssueCreated
                .GetFormattedString(id);
        }

        public string RemoveIssue(int issueId)
        {
            if (this.Data.CurrentlyLoggedIn == null)
            {
                return Messages
                    .CurrentlyNoUserLoggedInAtSystem;
            }

            if (!this.Data.IssuesById.ContainsKey(issueId))
            {
                return Messages.InvalidIssueId.GetFormattedString(issueId);
            }

            var issue = this.Data.IssuesById[issueId];
            if (!this.Data.IssuesByUser[this.Data.CurrentlyLoggedIn.Username].Contains(issue))
            {
                return Messages
                    .ThisIssueDoesntBelongToUser
                    .GetFormattedString(
                        issueId,
                        this.Data.CurrentlyLoggedIn.Username);
            }

            this.Data.RemoveIssue(issue, issueId);
            return Messages
                .IssueRemoved
                .GetFormattedString(issueId);
        }

        public string AddComment(int issueId, string text)
        {
            if (this.Data.CurrentlyLoggedIn == null)
            {
                return Messages.CurrentlyNoUserLoggedInAtSystem;
            }

            if (!this.Data.IssuesById.ContainsKey(issueId))
            {
                return Messages
                    .NoIssueWithGivenId
                    .GetFormattedString(issueId);
            }

            var issue = this.Data.IssuesById[issueId];
            var comment = new Comment(this.Data.CurrentlyLoggedIn, text);
            issue.AddComment(comment);
            this.Data.CommentsByUser[this.Data.CurrentlyLoggedIn].Add(comment);
            return Messages
                .SuccefullyAddedCommentToIssue
                .GetFormattedString(issueId);
        }

        public string GetMyIssues()
        {
            if (this.Data.CurrentlyLoggedIn == null)
            {
                return Messages.CurrentlyNoUserLoggedInAtSystem;
            }

            var issues = this.Data.IssuesByUser[this.Data.CurrentlyLoggedIn.Username];
            if (!issues.Any())
            {
                return Messages.NoIssues;
            }

            var orderedIssues = issues
                .OrderByDescending(x => x.Priority)
                .ThenBy(x => x.Title);
            return string.Join(
                Environment.NewLine,
                orderedIssues);
        }

        public string GetMyComments()
        {
            if (this.Data.CurrentlyLoggedIn == null)
            {
                return Messages.CurrentlyNoUserLoggedInAtSystem;
            }

            // Increasing performance by removing a bottleneck.
            // The comments for currently logged in user could be taken directly from dictionary
            // without no necessary lambda queries, which is the reason for bad performance.
            // ---------------------------------------------------------------
            var comments = this.Data.CommentsByUser[this.Data.CurrentlyLoggedIn];
            if (!comments.Any())
            {
                return Messages.NoComments;
            }

            var outPrintComments = comments.Select(c => c.ToString());
            return string.Join(Environment.NewLine, outPrintComments);
        }

        public string SearchForIssues(string[] tags)
        {
            if (tags.Length < 1)
            {
                return Messages.NoProvidedTags;
            }

            var issues = new List<IIssue>();

            foreach (var tag in tags)
            {
                issues.AddRange(this.Data.IssuesByTag[tag]);
            }

            if (!issues.Any())
            {
                return Messages.NoIssuesMatchingProvidedTags;
            }

            var distinctIssues = issues.Distinct();
            if (!distinctIssues.Any())
            {
                return Messages.NoIssues;
            }

            var orderedIssues = distinctIssues
                .OrderByDescending(i => i.Priority)
                .ThenBy(i => i.Title)
                .Select(i => i.ToString());
            return string.Join(
                Environment.NewLine,
                orderedIssues);
        }
    }
}