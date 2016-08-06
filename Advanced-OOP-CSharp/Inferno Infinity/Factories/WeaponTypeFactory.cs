namespace P10InfernoInfinity.Factories
{
    using System;
    using Interfaces;
    using Interfaces.Factories;
    using Models.Weapons.WeaponTypes;

    public class WeaponTypeFactory : IWeaponTypeFactory
    {
        public IWeaponTypeable CreateWeaponType(string type)
        {
            switch (type)
            {
                case "Axe":
                    return new AxeWeaponType();
                case "Sword":
                    return new SwordWeaponType();
                case "Knife":
                    return new KnifeWeaponType();
                default:
                    throw new ArgumentException("Unknown type.");
            }
        }
    }
}