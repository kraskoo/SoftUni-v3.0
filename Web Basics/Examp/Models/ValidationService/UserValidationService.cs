namespace Models.ValidationService
{
    using Interfaces;

    public abstract class UserValidationService<T> : ValidationServiceGeneric<T>
        where T : class, IUser, ILogin
    {
        protected UserValidationService(T entity) : base(entity)
        {
        }
    }
}