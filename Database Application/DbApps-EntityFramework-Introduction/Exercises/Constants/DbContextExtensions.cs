namespace Exercises.Constants
{
    using System.Data.Entity;

    public static class DbContextExtensions
    {
        public static T GetContextType<T>()
            where T : DbContext, new()
        {
            return new T();
        }
    }
}