namespace Data.Services
{
    using System.Linq;
    using Interfaces;
    using Models;
    using Models.BindingModels;
    using Models.Enums;
    using Models.Utilities;
    using Models.ValidationService;

    public class UserService : Service
    {
        private LoginValidationService loginValidationService;
        private RegisterUserValidationService registerUserValidationService;
        private bool isUnique;

        public UserService(IDataProvidable data) : base(data)
        {
            this.isUnique = false;
        }

        public void RegisterUser(RegistrationUserBindingModel rubm)
        {
            var role = this.Data.Users.GetAll().Any() ? UserRole.Regular : UserRole.Administrator;
            var newUser = rubm.GetMappedToRegisterUserBindingModel(role);
            this.Data.Users.InsertOrUpdate(newUser);
            this.Data.SaveChanges();
        }

        public bool IsLoginValid(LoginUserBindingModel lubm, string sessionId)
        {
            this.loginValidationService = new LoginValidationService(lubm, this.Data.Users.GetAll());
            var user = this.Data.Users.Find(u => u.Email == lubm.Email && u.Password == lubm.Password);
            if (this.loginValidationService.InvalidProperties.Count != 0 || user == null)
            {
                return false;
            }

            bool hasSessionWithCurrentId = this.GetUsedSessionIfExists(sessionId, user.Id);
            if (!hasSessionWithCurrentId)
            {
                var login = new Login
                {
                    User = user,
                    IsActive = true,
                    SessionId = sessionId
                };

                this.Data.Logins.InsertOrUpdate(login);
                this.Data.SaveChanges();
            }

            return true;
        }

        public bool IsRegistrationValid(RegistrationUserBindingModel rubm)
        {
            this.isUnique = this.IsUserUnique(rubm);
            this.registerUserValidationService = new RegisterUserValidationService(rubm, this.isUnique);
            return this.registerUserValidationService.InvalidProperties.Count == 0 && this.isUnique;
        }

        private bool GetUsedSessionIfExists(string sessionId, int id)
        {
            var login = this.Data.Logins.Find(l => l.SessionId == sessionId && l.UserId == id);
            var hasLogin = login != null;
            if (hasLogin)
            {
                login.IsActive = true;
                this.Data.SaveChanges();
            }

            return hasLogin;
        }

        private bool IsUserUnique(RegistrationUserBindingModel rubm)
        {
            return !this.Data.Users.GetAll().Any(u => u.Email == rubm.Email && u.Password == rubm.Password);
        }
    }
}