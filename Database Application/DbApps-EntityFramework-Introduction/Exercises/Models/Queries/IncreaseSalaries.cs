namespace Exercises.Models.Queries
{
    using System.Data.Entity;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem16 - Increase Salaries
    /// </summary>
    public class IncreaseSalaries : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var employeesByDepartment = context
                .Employees
                .Include(e => e.Department)
                .Where(e =>
                    e.Department.Name == "Engineering" ||
                    e.Department.Name == "Tool Design" ||
                    e.Department.Name == "Marketing" ||
                    e.Department.Name == "Information Services");
            foreach (var employee in employeesByDepartment)
            {
                employee.Salary += employee.Salary * 0.12m;
            }

            context.SaveChanges();
            var employeesProjection = employeesByDepartment
                .Select(e => new
                {
                    e.FirstName, e.LastName, e.Salary
                });

            foreach (var employee in employeesProjection)
            {
                this.Result
                    .AppendLine(
                        $"{employee.FirstName} {employee.LastName} (${employee.Salary:F6})");
            }

            return this.Result.ToString();
        }
    }
}