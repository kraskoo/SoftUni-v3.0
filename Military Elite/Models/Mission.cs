namespace P08MilitaryElite.Models
{
    using Interfaces;

    public class Mission : IMission
    {
        private const string DefaultMissionState = "inProgress";

        public Mission(string codeName, string state = DefaultMissionState)
        {
            this.CodeName = codeName;
            this.State = this.ValidateMissionState(state);
        }

        public string CodeName { get; }

        public string State { get; private set; }

        public bool CanExistCurrentMission()
        {
            return this.State != string.Empty;
        }

        public void CompleteMission()
        {
            this.State = "Finished";
        }

        public override string ToString()
        {
            return $"Code Name: {this.CodeName} State: {this.State}";
        }

        private string ValidateMissionState(string typeOfState)
        {
            if (typeOfState != "inProgress" && typeOfState != "Finished")
            {
                return string.Empty;
            }

            return typeOfState;
        }
    }
}