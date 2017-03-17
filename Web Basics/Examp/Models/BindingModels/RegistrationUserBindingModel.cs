namespace Models.BindingModels
{
    using Enums;
    using Interfaces;

    public class RegistrationUserBindingModel : IUser, ILogin
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public UserRole UserRole { get; set; }
    }
}