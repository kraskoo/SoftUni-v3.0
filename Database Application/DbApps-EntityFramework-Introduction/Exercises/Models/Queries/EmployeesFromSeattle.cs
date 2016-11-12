namespace Exercises.Models.Queries
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem05 - Employees from Seattle
    /// </summary>
    public class EmployeesFromSeattle : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var employees = context
                .Employees
                .Include(e => e.Department)
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName, e.LastName,
                    DepartmentName = e.Department.Name, e.Salary
                });
            foreach (var employee in employees)
            {
                this.Result.AppendFormat(
                    "{0} {1} from {2} - ${3:F2}{4}",
                    employee.FirstName,
                    employee.LastName,
                    employee.DepartmentName,
                    employee.Salary,
                    Environment.NewLine);
            }

            return this.Result.ToString();
        }
    }
}