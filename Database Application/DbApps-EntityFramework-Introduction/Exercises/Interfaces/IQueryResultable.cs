namespace Exercises.Interfaces
{
    using System.Data.Entity;

    public interface IQueryResultable<in T> where T : DbContext
    {
        string QueryResult(T context);
    }
}