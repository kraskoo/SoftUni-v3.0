namespace Models.ValidationService
{
    using System.Collections.Generic;

    public abstract class ValidationService
    {
        public abstract IDictionary<string, string> InvalidProperties { get; }

        protected abstract void AppendNewInvalidValidation(string reasonField, string invalidMessage);

        protected abstract void ExecuteValidations();
    }
}