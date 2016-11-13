namespace Exercises.Models.Queries
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Proble12 - *Native SQL Query (Part One)
    /// </summary>
    public class NativeSQLQueryCodeFirstPart : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var stopWatch = new Stopwatch();
            this.Result.AppendLine("Code first query");
            var codeFirstQuery = this.CodeFirstQuery(context);
            stopWatch.Start();
            this.Result.AppendLine($"[{string.Join(", ", codeFirstQuery)}]");
            this.Result.AppendLine($"Employees Count: {codeFirstQuery.Count}");
            this.Result.AppendLine($"Elapsed: {stopWatch.Elapsed}");
            stopWatch.Stop();
            return this.Result.ToString();
        }

        private ICollection<string> CodeFirstQuery(SoftuniContext context)
        {
            ICollection<string> resultQuery = new List<string>();
            var employees = context
                .Employees
                .Include(e => e.Projects)
                .Where(e => e.Projects.Count(p => p.StartDate.Year == 2002) > 0)
                .Select(e => e.FirstName);
            foreach (var employee in employees)
            {
                resultQuery.Add(employee);
            }

            return resultQuery;
        }
    }
}