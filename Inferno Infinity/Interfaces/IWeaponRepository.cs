namespace P10InfernoInfinity.Interfaces
{
    public interface IWeaponRepository
    {
        void CreateWeapon(string weaponType, string weaponName);

        void AddGemToWeapon(string weaponName, int socketIndex, string gemType);

        void RemoveGemFromWeapon(string weaponName, int socketIndex);

        string Print(string weaponName);
    }
}