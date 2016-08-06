namespace P10InfernoInfinity.Models.Weapons.WeaponLevels
{
    using System;
    using Enums;

    public class UncommonWeaponLevel : WeaponLevel
    {
        private const WeaponLevelEnumeration DefaultWeaponLevel =
            WeaponLevelEnumeration.Uncommon;
        private const string DefaultWeaponLevelAsString = "Uncommon";
        private const int DefaultDamageMultiplier = 2;

        public UncommonWeaponLevel()
            : base(DefaultWeaponLevel, DefaultDamageMultiplier)
        {
        }

        public UncommonWeaponLevel(string levelOfWeapon)
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