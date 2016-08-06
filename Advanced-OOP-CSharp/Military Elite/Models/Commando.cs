namespace P08MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Text;
    using Factories;
    using Interfaces;
    using Interfaces.Factories;

    public class Commando : SpecialisedSoldier, ICommando
    {
        private readonly IMissionFactory missionFactory;
        private readonly IList<IMission> missions;

        public Commando(
            string id,
            string firstName,
            string lastName,
            double salary,
            string corps,
            IMissionFactory missionFactory,
            params string[] missionsData) : base(
                id, firstName, lastName, salary, corps)
        {
            this.missions = new List<IMission>();
            this.missionFactory = missionFactory;
            this.SetMissions(missionsData);
        }

        public Commando(
            string id,
            string firstName,
            string lastName,
            double salary,
            string corps,
            params string[] missionsData) : this(
                id, firstName, lastName, salary, corps, new MissionFactory(), missionsData)
        {
        }

        public IEnumerable<IMission> Missions => this.missions;

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(base.ToString()).AppendLine("Missions:");
            for (int i = 0; i < this.missions.Count; i++)
            {
                output.AppendLine($"  {this.missions[i]}");
            }

            return output.ToString().Trim();
        }

        private void SetMissions(params string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                string codeName = data[i];
                string state = data[i + 1];
                IMission newMission = this.missionFactory.CreateMission(codeName, state);
                if (newMission != null)
                {
                    this.missions.Add(newMission);
                }
            }
        }
    }
}