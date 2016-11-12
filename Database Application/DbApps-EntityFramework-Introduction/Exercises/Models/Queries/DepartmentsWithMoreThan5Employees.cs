namespace Exercises.Models.Queries
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem11 - Departments with more than 5 employees
    /// </summary>
    public class DepartmentsWithMoreThan5Employees : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var departments = context
                .Departments
                .Include(d => d.Employees)
                .Include(d => d.Manager)
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .Select(d => new
                {
                    d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerInfo = d.Manager.DepartmentID == d.DepartmentID ?
                    new { d.Manager.LastName, d.Manager.JobTitle } : null,
                    Employees = d.Employees
                    .Where(e => e.EmployeeID != d.ManagerID)
                    .OrderBy(e => e.EmployeeID)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                });

            foreach (var department in departments)
            {
                this.Result.AppendLine($"{department.Name} {department.ManagerFirstName}");
                if (department.ManagerInfo != null)
                {
                    this.Result.AppendFormat(
                        "{0} {1} {2}{3}",
                        department.ManagerFirstName,
                        department.ManagerInfo.LastName,
                        department.ManagerInfo.JobTitle,
                        Environment.NewLine);
                }

                foreach (var employee in department.Employees)
                {
                    this.Result
                        .AppendLine(
                            $"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
                }
            }

            return this.Result.ToString();
        }
    }
}