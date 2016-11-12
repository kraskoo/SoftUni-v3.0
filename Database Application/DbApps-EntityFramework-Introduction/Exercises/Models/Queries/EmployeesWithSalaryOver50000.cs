namespace Exercises.Models.Queries
{
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem04 - Employees with Salary Over 50 000
    /// </summary>
    public class EmployeesWithSalaryOver50000 : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var employees = context
                .Employees
                .Where(e => e.Salary > 50000)
                .Select(e => e.FirstName);
            foreach (var employee in employees)
            {
                this.Result.AppendLine(employee);
            }

            return this.Result.ToString();
        }
    }
}