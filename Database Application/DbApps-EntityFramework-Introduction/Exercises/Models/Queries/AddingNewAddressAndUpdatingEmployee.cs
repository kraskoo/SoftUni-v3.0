namespace Exercises.Models.Queries
{
    using System.Data.Entity;
    using System.Linq;
    using CodeFirstFromDatabase;

    /// <summary>
    /// Problem06 - Adding a New Address and Updating Employee
    /// </summary>
    public class AddingNewAddressAndUpdatingEmployee : Query<SoftuniContext>
    {
        public override string QueryResult(SoftuniContext context)
        {
            var newAddress = new Address
            {
                AddressText = "Vitoshka 15",
                TownID = 4
            };

            context.Addresses.Add(newAddress);
            context
                .Employees
                .First(e => e.LastName == "Nakov")
                .Address = newAddress;
            context.SaveChanges();
            var employeeAddresses = context
                .Employees
                .Include(e => e.Address)
                .OrderByDescending(e => e.AddressID)
                .Take(10)
                .Select(e => e.Address.AddressText);
            foreach (var address in employeeAddresses)
            {
                this.Result.AppendLine(address);
            }

            return this.Result.ToString();
        }
    }
}