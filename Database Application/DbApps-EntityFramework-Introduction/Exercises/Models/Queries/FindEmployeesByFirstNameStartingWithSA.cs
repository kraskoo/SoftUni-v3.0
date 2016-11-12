namespace Exercises.Models.Queries
{
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem18 - Find Employees by First Name starting with ‘SA’
    /// </summary>
    public class FindEmployeesByFirstNameStartingWithSA : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var employees = context
                .Employees
                .Where(e => e.FirstName.ToUpper().StartsWith("SA"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                });

            foreach (var employee in employees)
            {
                this.Result.AppendLine(
                        $"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary})");
            }

            return this.Result.ToString();
        }
    }
}