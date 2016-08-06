namespace P10InfernoInfinity.Models.Weapons.WeaponLevels
{
    using System;
    using Enums;

    public class EpicWeaponLevel : WeaponLevel
    {
        private const WeaponLevelEnumeration DefaultWeaponLevel =
            WeaponLevelEnumeration.Epic;
        private const string DefaultWeaponLevelAsString = "Epic";
        private const int DefaultDamageMultiplier = 5;

        public EpicWeaponLevel()
            : base(DefaultWeaponLevel, DefaultDamageMultiplier)
        {
        }

        public EpicWeaponLevel(string levelOfWeapon)
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