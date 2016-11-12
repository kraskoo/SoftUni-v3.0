namespace Exercises.Models.Queries
{
    using System.Data.Entity;
    using System.Text;
    using Interfaces;

    public abstract class Query<T> : IQueryResultable<T>
        where T : DbContext
    {
        protected Query()
        {
            this.Result = new StringBuilder();
        }

        protected StringBuilder Result { get; }

        public abstract string QueryResult(T context);
    }
}