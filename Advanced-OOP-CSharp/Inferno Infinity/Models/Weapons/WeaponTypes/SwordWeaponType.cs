namespace P10InfernoInfinity.Models.Weapons.WeaponTypes
{
    using System;
    using Enums;

    public class SwordWeaponType : WeaponType
    {
        private const WeaponTypeEnumeration WeaponType = WeaponTypeEnumeration.Sword;
        private const int DefaultMinDamage = 4;
        private const int DefaultMaxDamage = 6;
        private const int DefaultSocketCount = 3;

        public SwordWeaponType() : base(
                WeaponType, DefaultMinDamage, DefaultMaxDamage, DefaultSocketCount)
        {
        }

        public SwordWeaponType(string type) : base(
            DefaultMinDamage, DefaultMaxDamage, DefaultSocketCount)
        {
            base.TypeOfWeapon = base.ParseWeaponType(this.ValidateWeaponType(type));
        }

        protected override sealed string ValidateWeaponType(string type)
        {
            if (type != WeaponType.ToString())
            {
                throw new ArgumentException(DefaultErrorTypeMessage);
            }

            return type;
        }
    }
}