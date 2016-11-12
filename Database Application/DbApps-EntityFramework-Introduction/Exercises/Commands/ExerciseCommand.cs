namespace Exercises.Commands
{
    using System.Data.Entity;
    using Interfaces;

    public class ExerciseCommand<T> : IExerciseExecutable<T>
        where T : DbContext
    {
        private readonly T context;

        public ExerciseCommand(T context)
        {
            this.context = context;
        }

        public void Execute(IQueryResultable<T> queryResultable, IOutputWriter writer)
        {
            writer.Write(queryResultable.QueryResult(this.context));
        }
    }
}