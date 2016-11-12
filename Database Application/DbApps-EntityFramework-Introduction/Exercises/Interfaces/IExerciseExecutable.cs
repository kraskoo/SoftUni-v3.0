namespace Exercises.Interfaces
{
    using System.Data.Entity;

    public interface IExerciseExecutable<out T> where T : DbContext
    {
        void Execute(IQueryResultable<T> queryResultable, IOutputWriter writer);
    }
}