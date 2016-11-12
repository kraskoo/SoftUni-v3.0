namespace Exercises.Models.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem10 - Employee with id 147 sorted by project names
    /// </summary>
    public class EmployeeWithId147SortedByProjectNames : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var employee = context
                .Employees
                .Include(e => e.Projects)
                .FirstOrDefault(e => e.EmployeeID == 147);
            if (employee != null)
            {
                this.Result.AppendLine(
                string.Format(
                    "{0} {1} {2}{3}{4}{5}",
                    employee.FirstName,
                    employee.LastName,
                    employee.JobTitle,
                    Environment.NewLine,
                    string.Join(Environment.NewLine, GetProjectNames(employee)),
                    Environment.NewLine));
            }
            
            return this.Result.ToString();
        }

        private static IEnumerable<string> GetProjectNames(Employee employee)
        {
            return employee
                .Projects
                .OrderBy(p => p.Name)
                .Select(p => $"{p.Name}");
        }
    }
}