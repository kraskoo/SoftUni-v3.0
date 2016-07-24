namespace P10InfernoInfinity.Models.Weapons.WeaponTypes
{
    using System;
    using Enums;

    public class KnifeWeaponType : WeaponType
    {
        private const WeaponTypeEnumeration WeaponType = WeaponTypeEnumeration.Knife;
        private const int DefaultMinDamage = 3;
        private const int DefaultMaxDamage = 4;
        private const int DefaultSocketCount = 2;

        public KnifeWeaponType() : base(
                WeaponType, DefaultMinDamage, DefaultMaxDamage, DefaultSocketCount)
        {
        }

        public KnifeWeaponType(string type) : base(
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