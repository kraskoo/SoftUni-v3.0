namespace P10InfernoInfinity.Factories
{
    using System;
    using Interfaces;
    using Interfaces.Factories;
    using Models.Weapons.WeaponLevels;

    public class WeaponLevelFactory : IWeaponLevelFactory
    {
        public IWeaponLevelable CreateWeaponLevel(string type)
        {
            switch (type)
            {
                case "Common":
                    return new CommonWeaponLevel();
                case "Uncommon":
                    return new UncommonWeaponLevel();
                case "Rare":
                    return new RareWeaponLevel();
                case "Epic":
                    return new EpicWeaponLevel();
                default:
                    throw new ArgumentException("Unknown type.");
            }
        }
    }
}