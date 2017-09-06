namespace BuhtigIssueTracker.Interfaces
{
    using System.Collections.Generic;
    using Enums;

    public interface IIssue
    {
        string Title { get; }

        string Description { get; }

        IssuePriority Priority { get; }

        IEnumerable<string> Tags { get; }

        IEnumerable<IComment> Comments { get; }

        void AddTag(string tag);

        void AddComment(IComment comment);

        void RemoveComment(IComment comment);
    }
}