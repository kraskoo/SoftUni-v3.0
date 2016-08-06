namespace P08MilitaryElite.Models
{
    using Interfaces;

    public class Part : IPart
    {
        public Part(string partName, int workedHours)
        {
            this.PartName = partName;
            this.WorkedHours = workedHours;
        }

        public string PartName { get; }

        public int WorkedHours { get; }

        public override string ToString()
        {
            return $"Part Name: {this.PartName} Hours Worked: {this.WorkedHours}";
        }
    }
}