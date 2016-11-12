namespace Exercises.Models.Queries
{
    using System;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem15 - Find Latest 10 Projects
    /// </summary>
    public class FindLatest10Projects : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var projects = context
                .Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Name, p.Description, p.StartDate, p.EndDate
                });
            foreach (var project in projects)
            {
                this.Result
                    .AppendFormat(
                        "{0} {1} {2} {3}{4}",
                        project.Name,
                        project.Description,
                        this.GetFormattedDate(project.StartDate),
                        this.GetFormattedDate(project.EndDate),
                        Environment.NewLine);
            }

            return this.Result.ToString();
        }

        private string GetFormattedDate(DateTime? date)
        {
            return $"{date:M'/'d'/'yyyy h:mm:ss tt}";
        }
    }
}