namespace P08MilitaryElite.Models
{
    using Interfaces;

    public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        protected SpecialisedSoldier(
            string id,
            string firstName,
            string lastName,
            double salary,
            string corps) : base(
                id, firstName, lastName, salary)
        {
            this.Corps = this.ValidateCorps(corps);
        }

        public string Corps { get; }

        public bool CanExistSpecialisedSoldier()
        {
            return this.Corps != string.Empty;
        }

        public override string ToString()
        {
            return $"{base.ToString()}\nCorps: {this.Corps}";
        }

        private string ValidateCorps(string type)
        {
            if (type != "Airforces" && type != "Marines")
            {
                return string.Empty;
            }

            return type;
        }
    }
}