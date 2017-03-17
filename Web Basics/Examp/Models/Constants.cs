namespace Models
{
    public class Constants
    {
        public const string TakenEmailMessage = "This email is already taken.";

        public const string InvalidEmailMessage = "Email field require to contains these symbols '@', and '.'";

        public const string InvalidPasswordMessage =
            "The password length must be at least 6 symbols and must contain at least 1 uppercase, 1 lowercase letter and 1 digit";

        public const string InvalidConfirmMessage =
            "Confirm password should be exactly as the password you enter in previous field.";

        public const string InvalidFullnameMessage = "Full name field cannot be empty.";

        public const string InvalidLoginEmail = "This email is not registred.";

        public const string InvalidLoginPassword = "This password don't match to current email.";

        public const string InvalidGameTitleMessage =
            "The title has to begin with uppercase letter and has length between 3 and 100 symbols (inclusive)";

        public const string InvalidGamePriceMessage = "The price must be positive number";

        public const string InvalidGameSizeMessage = "The size must be positive number";

        public const string InvalidGameTrailerMessage = "The youtube id must be exactly 11 characters long.";

        public const string InvalidGameImageThumbnailMessage =
            "Image Thumbnail should be a plain text starting with http://, https://.";

        public const string InvalidGameYoutubeId = "The youtube id must be exactly 11 characters long.";

        public const string InvalidGameDescription = "Description must be at least 20 symbols";
    }
}