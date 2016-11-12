namespace Exercises.Models.Queries
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem08 - Find employees in period
    /// </summary>
    public class FindEmployeesInPeriod : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var employees = context
                .Employees
                .Include(e => e.Projects)
                .Include(e => e.Manager)
                .Where(e => e.Projects
                    .Count(p =>
                        p.StartDate.Year >= 2001 &&
                        p.StartDate.Year <= 2003) > 0)
                .Take(30);
            foreach (var employee in employees)
            {
                this.Result.AppendFormat(
                    "{0} {1} {2}{3}{4}{5}",
                    employee.FirstName,
                    employee.LastName,
                    employee.Manager.FirstName,
                    Environment.NewLine,
                    this.GetFormattedProjects(employee),
                    Environment.NewLine);
            }

            return this.Result.ToString();
        }

        private string GetFormattedProjects(Employee employee)
        {
            return string.Join(
                Environment.NewLine,
                employee.Projects.Select(p =>
                    string.Format(
                        "--{0} {1} {2}",
                        p.Name,
                        this.GetCorrectFormattedDate(p.StartDate),
                        this.GetCorrectFormattedDate(p.EndDate))));
        }

        private string GetCorrectFormattedDate(DateTime? date)
        {
            return $"{date:M'/'d'/'yyyy hh':'mm':'ss tt}";
        }
    }
}