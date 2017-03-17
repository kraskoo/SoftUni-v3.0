namespace Models.BindingModels
{
    using Enums;
    using Interfaces;

    public class LoginUserBindingModel : ILogin, IUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public UserRole UserRole { get; set; }
    }
}