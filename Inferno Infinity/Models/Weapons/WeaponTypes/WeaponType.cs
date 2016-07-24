namespace P10InfernoInfinity.Models.Weapons.WeaponTypes
{
    using System;
    using Enums;
    using Interfaces;

    public abstract class WeaponType : IWeaponTypeable
    {
        protected const string DefaultErrorTypeMessage = "Wrong weapon type.";

        protected WeaponType(
            WeaponTypeEnumeration typeOfWeapon,
            int minDamage,
            int maxDamage,
            int socketCount) : this(minDamage, maxDamage, socketCount)
        {
            this.TypeOfWeapon = typeOfWeapon;
        }

        protected WeaponType(int minDamage, int maxDamage, int socketCount)
        {
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.SocketCount = socketCount;
        }

        public WeaponTypeEnumeration TypeOfWeapon { get; protected set; }

        public int MinDamage { get; protected set; }

        public int MaxDamage { get; protected set; }

        public int SocketCount { get; }

        protected abstract string ValidateWeaponType(string type);

        protected WeaponTypeEnumeration ParseWeaponType(string weaponType)
        {
            return
                (WeaponTypeEnumeration)Enum
                    .Parse(
                        typeof(WeaponTypeEnumeration),
                        weaponType
                    );
        }
    }
}