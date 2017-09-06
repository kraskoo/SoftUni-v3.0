namespace BuhtigIssueTracker.Constants
{
    public class Messages
    {
        public const string AlreadyLoggedInUser =
            "There is already a logged in user";

        public const string ProvidedPasswordDoesntMatch =
            "The provided passwords do not match";

        public const string SuccessfulRegisteredUser =
            "User {0} registered successfully";

        public const string AlreadyRegistredUser =
            "A user with username {0} already exists";

        public const string UserIsNotRegistredAtTheSystem =
            "A user with username {0} does not exist";

        public const string InvalidPassword =
            "The password is invalid for user {0}";

        public const string SuccessfullyLoggedIn =
            "User {0} logged in successfully";

        public const string CurrentlyNoUserLoggedInAtSystem =
            "There is no currently logged in user";

        public const string SuccessfullyLogOut =
            "User {0} logged out successfully";

        public const string SuccessfullyIssueCreated =
            "Issue {0} created successfully";

        public const string InvalidIssueId =
            "There is no issue with ID {0}";

        public const string NoIssueWithGivenId =
            "There is no issue with ID {0}";

        public const string SuccefullyAddedCommentToIssue =
            "Comment added successfully to issue {0}";

        public const string NoIssues = "No issues";

        public const string NoComments = "No comments";

        public const string NoProvidedTags = "There are no tags provided";

        public const string NoIssuesMatchingProvidedTags =
            "There are no issues matching the tags provided";

        public const string IssueRemoved =
            "Issue {0} removed";

        public const string ThisIssueDoesntBelongToUser = "The issue with ID {0} does not belong to user {1}";
    }
}