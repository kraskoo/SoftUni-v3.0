namespace P08MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interfaces;

    public class LeutenantGeneral : Private, ILeutenantGeneral
    {
        private readonly ICollection<IPrivate> outterPrivates;
        private readonly ICollection<IPrivate> underCommandPrivates;
        private readonly string[] idsOfPrivateSet;

        public LeutenantGeneral(
            string id,
            string firstName,
            string lastName,
            double salary,
            ICollection<IPrivate> outterPrivets,
            params string[] ids) : base(
                id, firstName, lastName, salary)
        {
            this.outterPrivates = outterPrivets;
            this.idsOfPrivateSet = ids;
            this.underCommandPrivates = this.GetUnderCommandPrivates();
        }

        public IEnumerable<IPrivate> Privates => this.underCommandPrivates;

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(base.ToString());
            output.AppendLine("Privates:");
            foreach (IPrivate commandPrivate in this.underCommandPrivates)
            {
                output.AppendLine($"  {commandPrivate}");
            }

            return output.ToString().Trim();
        }

        private IList<IPrivate> GetUnderCommandPrivates()
        {
            IList<IPrivate> commandedPrivates = new List<IPrivate>();
            foreach (string currentId in this.idsOfPrivateSet)
            {
                IPrivate currentPrivate =
                    this.outterPrivates
                        .FirstOrDefault(p => p.Id.Equals(currentId));
                if (currentPrivate != null)
                {
                    commandedPrivates.Add(currentPrivate);
                }
            }

            return commandedPrivates;
        }
    }
}