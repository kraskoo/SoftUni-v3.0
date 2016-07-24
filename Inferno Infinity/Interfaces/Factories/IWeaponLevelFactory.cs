namespace P10InfernoInfinity.Interfaces.Factories
{
    public interface IWeaponLevelFactory
    {
        IWeaponLevelable CreateWeaponLevel(string type);
    }
}