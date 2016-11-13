namespace Exercises.Models.Queries
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Proble12 - *Native SQL Query (Part Two)
    /// </summary>
    public class NativeSQLQueryNativeQueryPart : Query<SoftuniContext>
    {
        private readonly string queryString;

        public NativeSQLQueryNativeQueryPart()
        {
            this.queryString = string.Format(
                "{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                "SELECT e.FirstName",
                "FROM (SELECT e.FirstName,",
                "(SELECT COUNT(1) AS [Count]",
                "FROM  EmployeesProjects ep",
                "INNER JOIN Projects p ON ep.ProjectID = p.ProjectID",
                "WHERE (e.EmployeeID = ep.EmployeeID) AND",
                "(2002 = (DATEPART (year, p.StartDate)))) q",
                "FROM Employees AS e)  AS e",
                "WHERE q > 0");
        }

        public override string QueryResult(SoftuniContext context)
        {
            var stopWatch = new Stopwatch();
            this.Result.AppendLine("Native query");
            var nativeQuery = this.NativeQuery(context);
            stopWatch.Start();
            this.Result.AppendLine($"[{string.Join(", ", nativeQuery)}]");
            this.Result.AppendLine($"Employees Count: {nativeQuery.Count}");
            this.Result.AppendLine($"Elapsed: {stopWatch.Elapsed}");
            stopWatch.Stop();
            return this.Result.ToString();
        }

        private ICollection<string> NativeQuery(SoftuniContext context)
        {
            ICollection<string> resultQuery = new List<string>();
            var employees = context
                .Database
                .SqlQuery<string>(this.queryString);
            foreach (var employee in employees)
            {
                resultQuery.Add(employee);
            }

            return resultQuery;
        }
    }
}