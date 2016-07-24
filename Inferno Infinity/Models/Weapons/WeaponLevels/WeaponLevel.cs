namespace P10InfernoInfinity.Models.Weapons.WeaponLevels
{
    using System;
    using Enums;
    using Interfaces;

    public abstract class WeaponLevel : IWeaponLevelable
    {
        protected const string DefaultWrongLevelMessage = "Wrong level of weapon.";

        protected WeaponLevel(WeaponLevelEnumeration levelOfWeapon, int damageMultiplier)
        {
            this.LevelOfWeapon = levelOfWeapon;
            this.DamageMultiplier = damageMultiplier;
        }

        protected WeaponLevel(int damageMultiplier)
        {
            this.DamageMultiplier = damageMultiplier;
        }

        public WeaponLevelEnumeration LevelOfWeapon { get; protected set; }

        public int DamageMultiplier { get; protected set; }

        protected abstract string ValidateLevel(string level);

        protected WeaponLevelEnumeration ParseWeaponLevel(string levelOfWeapon)
        {
            return
                (WeaponLevelEnumeration)Enum
                    .Parse(
                        typeof(WeaponLevelEnumeration),
                        levelOfWeapon
                    );
        }
    }
}