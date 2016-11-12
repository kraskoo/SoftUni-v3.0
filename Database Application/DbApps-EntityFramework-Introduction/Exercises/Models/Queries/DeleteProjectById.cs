namespace Exercises.Models.Queries
{
    using System.Data.Entity;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem07 - Delete Project by Id
    /// </summary>
    public class DeleteProjectById : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var project = context.Projects.Find(2);
            var employeesWorkOnProject = context
                .Employees
                .Include(e => e.Projects)
                .Where(e => e.Projects.Any(p => p.ProjectID == project.ProjectID));
            foreach (var employee in employeesWorkOnProject)
            {
                employee.Projects.Remove(project);
            }

            context.Projects.Remove(project);
            context.SaveChanges();
            var projects = context.Projects.Take(10).Select(p => p.Name);
            foreach (var selectedProjects in projects)
            {
                this.Result.AppendLine(selectedProjects);
            }

            return this.Result.ToString();
        }
    }
}