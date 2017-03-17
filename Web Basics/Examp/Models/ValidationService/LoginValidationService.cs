namespace Models.ValidationService
{
    using System.Collections.Generic;
    using System.Linq;
    using BindingModels;

    public class LoginValidationService : UserValidationService<LoginUserBindingModel>
    {
        private readonly IDictionary<string, string> invalidProperties;
        private readonly IEnumerable<User> users;

        public LoginValidationService(
            LoginUserBindingModel loginUserBindingModel, IEnumerable<User> users) : base(
            loginUserBindingModel)
        {
            this.users = users;
            this.invalidProperties = new Dictionary<string, string>();
            this.ExecuteValidations();
        }

        public override IDictionary<string, string> InvalidProperties => new Dictionary<string, string>(this.invalidProperties);

        protected override void AppendNewInvalidValidation(string reasonField, string invalidMessage)
        {
            if (!string.IsNullOrEmpty(invalidMessage))
            {
                this.invalidProperties.Add(reasonField, invalidMessage);
            }
        }

        protected sealed override void ExecuteValidations()
        {
            this.ValidateEmailAndPassword();
        }

        private string ValidateEmail()
        {
            User user = this.users.FirstOrDefault(login => login.Email == this.BindingModel.Email);
            Validation validation = new Validation();
            validation.CheckUp(user != null, Constants.InvalidLoginEmail);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Email), validation.ToString());
            return user?.Email;
        }

        private void ValidateEmailAndPassword()
        {
            string email = this.ValidateEmail();
            Validation validation = new Validation();
            User user =
                this.users.FirstOrDefault(
                    u => u.Password == this.BindingModel.Password &&
                    u.Email == email);
            validation.CheckUp(user != null, Constants.InvalidLoginPassword);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Password), validation.ToString());
        }
    }
}