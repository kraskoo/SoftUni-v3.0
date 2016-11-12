namespace Exercises.Models.Queries
{
    using System.Linq;
    using GringottsCodeFirstFromDatabase;

    /// <summary>
    /// Problem19 - First Letter
    /// </summary>
    public class FirstLetter : Query<GringottsContext>
    {
        public override string QueryResult(GringottsContext context)
        {
            var wizzards = context
                .WizzardDeposits
                .Where(wd => wd.DepositGroup == "Troll Chest")
                .OrderBy(wd => wd.FirstName)
                .Select(wd => wd.FirstName.Substring(0, 1))
                .Distinct();

            foreach (var wizzard in wizzards)
            {
                this.Result.AppendLine($"{wizzard}");
            }

            return this.Result.ToString();
        }
    }
}