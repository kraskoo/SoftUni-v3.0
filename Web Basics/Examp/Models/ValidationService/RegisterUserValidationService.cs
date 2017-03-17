namespace Models.ValidationService
{
    using System.Collections.Generic;
    using System.Linq;
    using BindingModels;

    public class RegisterUserValidationService : UserValidationService<RegistrationUserBindingModel>
    {
        private readonly IDictionary<string, string> validations;
        private readonly bool isUnique;

        public RegisterUserValidationService(
            RegistrationUserBindingModel registrationUserBindingModel,
            bool isUnique) : base(
                  registrationUserBindingModel)
        {
            this.isUnique = isUnique;
            this.validations = new Dictionary<string, string>();
            this.ExecuteValidations();
        }

        public override IDictionary<string, string> InvalidProperties =>
            new Dictionary<string, string>(this.validations);

        protected override void AppendNewInvalidValidation(string reasonField, string invalidMessage)
        {
            if (!string.IsNullOrEmpty(invalidMessage))
            {
                this.validations.Add(reasonField, invalidMessage);
            }
        }

        protected sealed override void ExecuteValidations()
        {
            this.ValidateEmail();
            this.ValidatePassword();
            this.ValidateConfirmPassword();
            this.ValidateFullName();
        }

        private void ValidateEmail()
        {
            Validation validation = new Validation();
            validation.CheckUp(this.isUnique, Constants.TakenEmailMessage);
            string validationResult = validation.ToString();
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Password), validationResult);
            validation = new Validation();
            validation.CheckUp(
                this.BindingModel.Email.Contains("@") && this.BindingModel.Email.Contains("."),
                Constants.InvalidEmailMessage);
            validationResult = validation.ToString();
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Email), validationResult);
        }

        private void ValidatePassword()
        {
            string password = this.BindingModel.Password;
            Validation validation = new Validation();
            validation.CheckUp(
                password.Length >= 6 &&
                password.Any(char.IsUpper) &&
                password.Any(char.IsLower) &&
                password.Any(char.IsDigit),
                Constants.InvalidPasswordMessage);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Password), validation.ToString());
        }

        private void ValidateConfirmPassword()
        {
            Validation validation = new Validation();
            validation.CheckUp(
                this.BindingModel.Password == this.BindingModel.ConfirmPassword,
                Constants.InvalidConfirmMessage);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.ConfirmPassword), validation.ToString());
        }

        private void ValidateFullName()
        {
            Validation validation = new Validation();
            validation.CheckUp(
                !string.IsNullOrEmpty(this.BindingModel.FullName.Trim(' ', '\t', '\n')),
                Constants.InvalidFullnameMessage);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.FullName), validation.ToString());
        }
    }
}