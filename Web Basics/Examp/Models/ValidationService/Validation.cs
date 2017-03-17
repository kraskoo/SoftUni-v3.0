namespace Models.ValidationService
{
    using System;

    public class Validation
    {
        private string message;

        public void CheckUp<T>(Predicate<T> condition, T target, string errorMessage)
        {
            if (!condition(target))
            {
                this.message = errorMessage;
            }
        }

        public void CheckUp(Func<bool> condition, string errorMessage)
        {
            if (!condition())
            {
                this.message = errorMessage;
            }
        }

        public void CheckUp(bool isCorrectResult, string errorMessage)
        {
            if (!isCorrectResult)
            {
                this.message = errorMessage;
            }
        }

        public override string ToString()
        {
            return this.message;
        }
    }
}