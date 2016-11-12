namespace Exercises.Models.Queries
{
    using System;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem03 - Employees full information
    /// </summary>
    public class EmployeesFullInformation : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.MiddleName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                });
            foreach (var employee in employees)
            {
                this.Result.AppendFormat(
                    "{0} {1} {2} {3} {4}{5}",
                    employee.FirstName,
                    employee.MiddleName,
                    employee.LastName,
                    employee.JobTitle,
                    employee.Salary,
                    Environment.NewLine);
            }

            return this.Result.ToString();
        }
    }
}