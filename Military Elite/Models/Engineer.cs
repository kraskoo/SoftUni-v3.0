namespace P08MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Text;
    using Factories;
    using Interfaces;
    using Interfaces.Factories;

    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private readonly IPartFactory partFactory;
        private readonly IList<IPart> parts;

        public Engineer(
            string id,
            string firstName,
            string lastName,
            double salary,
            string corps,
            IPartFactory partFactory,
            params string[] partsData) : base(
                id, firstName, lastName, salary, corps)
        {
            this.partFactory = partFactory;
            this.parts = new List<IPart>();
            this.GetParts(partsData);
        }

        public Engineer(
            string id,
            string firstName,
            string lastName,
            double salary,
            string corps,
            params string[] partsData) : this(
                id, firstName, lastName, salary, corps, new PartFactory(), partsData)
        {
        }

        public IEnumerable<IPart> Repairs => this.parts;

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(base.ToString()).AppendLine("Repairs:");
            for (int i = 0; i < this.parts.Count; i++)
            {
                output.AppendLine($"  {this.parts[i]}");
            }

            return output.ToString().Trim();
        }

        private void GetParts(params string[] partsData)
        {
            for (int i = 0; i < partsData.Length; i += 2)
            {
                IPart part =
                    this.partFactory
                        .CreatePart(partsData[i], int.Parse(partsData[i + 1]));
                this.parts.Add(part);
            }
        }
    }
}