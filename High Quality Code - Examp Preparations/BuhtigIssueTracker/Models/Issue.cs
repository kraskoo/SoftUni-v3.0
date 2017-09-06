namespace BuhtigIssueTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Enums;
    using Interfaces;

    public class Issue : IIssue
    {
        private readonly ISet<IComment> comments;
        private readonly ISet<string> tags;
        private string title;
        private string description;

        public Issue(string title, string description, IssuePriority priority, IEnumerable<string> tags)
        {
            this.Title = title;
            this.Description = description;
            this.Priority = priority;
            this.tags = new HashSet<string>(tags);
            this.comments = new HashSet<IComment>();
        }

        public int Id { get; set; }

        public string Title
        {
            get
            {
                return this.title;
            }

            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                {
                    throw new ArgumentException("The title must be at least 3 symbols long");
                }

                this.title = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException("The description must be at least 5 symbols long");
                }

                this.description = value;
            }
        }

        public IssuePriority Priority { get; }

        public IEnumerable<IComment> Comments => this.comments;

        public IEnumerable<string> Tags => this.tags;

        public void AddTag(string tag)
        {
            this.tags.Add(tag);
        }

        public void AddComment(IComment comment)
        {
            this.comments.Add(comment);
        }

        public void RemoveComment(IComment comment)
        {
            this.comments.Remove(comment);
        }

        public override string ToString()
        {
            var issue = new StringBuilder()
                .AppendLine(this.Title)
                .AppendFormat(
                    "Priority: {0}{1}",
                    this.GetPriorityValue(),
                    Environment.NewLine)
                .AppendLine(this.Description);

            if (this.tags.Count > 0)
            {
                var orderedTags = this.Tags.OrderBy(t => t);
                issue
                    .AppendFormat(
                        "Tags: {0}",
                        string.Join(",", orderedTags))
                    .AppendLine();
            }

            if (this.comments.Count > 0)
            {
                issue.AppendFormat(
                        "Comments:{0}{1}",
                        Environment.NewLine,
                        string.Join(Environment.NewLine, this.Comments))
                    .AppendLine();
            }

            return issue.ToString().Trim();
        }

        private string GetPriorityValue()
        {
            switch (this.Priority)
            {
                case IssuePriority.Showstopper:
                    return "****";
                case IssuePriority.High:
                    return "***";
                case IssuePriority.Medium:
                    return "**";
                case IssuePriority.Low:
                    return "*";
                default:
                    throw new InvalidOperationException("The priority is invalid");
            }
        }
    }
}