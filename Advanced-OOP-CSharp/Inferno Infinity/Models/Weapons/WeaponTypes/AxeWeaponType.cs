namespace P10InfernoInfinity.Models.Weapons.WeaponTypes
{
    using System;
    using Enums;

    public class AxeWeaponType : WeaponType
    {
        private const WeaponTypeEnumeration WeaponType = WeaponTypeEnumeration.Axe;
        private const int DefaultMinDamage = 5;
        private const int DefaultMaxDamage = 10;
        private const int DefaultSocketCount = 4;

        public AxeWeaponType() : base(
                WeaponType, DefaultMinDamage, DefaultMaxDamage, DefaultSocketCount)
        {
        }

        public AxeWeaponType(string type)
            : base(DefaultMinDamage, DefaultMaxDamage, DefaultSocketCount)
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