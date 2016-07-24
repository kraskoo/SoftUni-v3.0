namespace P10InfernoInfinity.Models.Weapons.WeaponLevels
{
    using System;
    using Enums;

    public class CommonWeaponLevel : WeaponLevel
    {
        private const WeaponLevelEnumeration DefaultWeaponLevel =
            WeaponLevelEnumeration.Common;
        private const string DefaultWeaponLevelAsString = "Common";
        private const int DefaultDamageMultiplier = 1;

        public CommonWeaponLevel()
            : base(DefaultWeaponLevel, DefaultDamageMultiplier)
        {
        }

        public CommonWeaponLevel(string levelOfWeapon)
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