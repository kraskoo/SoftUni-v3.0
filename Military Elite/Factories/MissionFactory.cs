namespace P08MilitaryElite.Factories
{
    using Interfaces;
    using Interfaces.Factories;
    using Models;

    public class MissionFactory : IMissionFactory
    {
        public IMission CreateMission(string codeName, string state)
        {
            IMission mission = new Mission(codeName, state);
            if (!mission.CanExistCurrentMission())
            {
                return null;
            }

            return mission;
        }
    }
}