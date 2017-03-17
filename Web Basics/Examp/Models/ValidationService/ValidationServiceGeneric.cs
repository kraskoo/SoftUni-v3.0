namespace Models.ValidationService
{
    using Interfaces;

    public abstract class ValidationServiceGeneric<T> : ValidationService where T : class, IModel
    {
        protected ValidationServiceGeneric(T bindingModel)
        {
            this.BindingModel = bindingModel;
        }

        protected T BindingModel { get; }
    }
}