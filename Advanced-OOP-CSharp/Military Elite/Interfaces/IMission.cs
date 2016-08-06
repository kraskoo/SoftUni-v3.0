namespace P08MilitaryElite.Interfaces
{
    public interface IMission
    {
        string CodeName { get; }

        string State { get; }

        bool CanExistCurrentMission();

        void CompleteMission();
    }
}