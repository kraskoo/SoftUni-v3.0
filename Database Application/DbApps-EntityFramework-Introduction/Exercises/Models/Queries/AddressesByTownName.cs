namespace Exercises.Models.Queries
{
    using System.Data.Entity;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem09 - Addresses by town name
    /// </summary>
    public class AddressesByTownName : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var addresses = context
                .Addresses
                .Include(a => a.Employees)
                .Include(a => a.Town)
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Take(10)
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    EmployeesCount = a.Employees.Count
                });

            foreach (var address in addresses)
            {
                this.Result.AppendLine(
                    $"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
            }

            return this.Result.ToString();
        }
    }
}