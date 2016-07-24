namespace P10InfernoInfinity.Interfaces.Factories
{
    public interface IWeaponTypeFactory
    {
        IWeaponTypeable CreateWeaponType(string type);
    }
}