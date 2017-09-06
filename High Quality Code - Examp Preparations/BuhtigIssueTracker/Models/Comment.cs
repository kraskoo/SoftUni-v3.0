namespace BuhtigIssueTracker.Models
{
    using System;
    using System.Text;
    using Interfaces;

    public class Comment : IComment
    {
        private string text;

        public Comment(IUser author, string text)
        {
            this.Author = author;
            this.Text = text;
        }

        public IUser Author { get; }

        public string Text
        {
            get
            {
                return this.text;
            }

            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 2)
                {
                    throw new ArgumentException("The text must be at least 2 symbols long");
                }

                this.text = value;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine(this.Text)
                .AppendFormat("-- {0}", this.Author.Username)
                .AppendLine()
                .ToString()
                .Trim();
        }
    }
}