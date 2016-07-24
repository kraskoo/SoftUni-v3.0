namespace P10InfernoInfinity.Models.Weapons.WeaponLevels
{
    using System;
    using Enums;

    public class RareWeaponLevel : WeaponLevel
    {
        private const WeaponLevelEnumeration DefaultWeaponLevel =
            WeaponLevelEnumeration.Rare;
        private const string DefaultWeaponLevelAsString = "Rare";
        private const int DefaultDamageMultiplier = 3;

        public RareWeaponLevel()
            : base(DefaultWeaponLevel, DefaultDamageMultiplier)
        {
        }

        public RareWeaponLevel(string levelOfWeapon)
            : base(DefaultDamageMultiplier)
        {
            base.LevelOfWeapon =
                base.ParseWeaponLevel(this.ValidateLevel(levelOfWeapon));
        }

        protected override sealed string ValidateLevel(string level)
        {
            if (level != DefaultWeaponLevelAsString)
            {
                throw new ArgumentException(DefaultWrongLevelMessage);
            }

            return level;
        }
    }
}