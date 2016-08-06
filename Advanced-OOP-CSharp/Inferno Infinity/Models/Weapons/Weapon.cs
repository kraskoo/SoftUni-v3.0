namespace P10InfernoInfinity.Models.Weapons
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class Weapon : IWeapon
    {
        private readonly IWeaponTypeable weaponTypeable;
        private readonly IWeaponLevelable weaponLevelable;
        private readonly IGem[] gems;

        public Weapon(IWeaponTypeable weaponTypeable, IWeaponLevelable weaponLevelable)
        {
            this.weaponTypeable = weaponTypeable;
            this.weaponLevelable = weaponLevelable;
            this.SocketCount = this.weaponTypeable.SocketCount;
            this.gems = new IGem[this.SocketCount];
        }

        public IEnumerable<IGem> Gems => this.gems;

        public int MinDamage =>
            (this.weaponTypeable.MinDamage * this.weaponLevelable.DamageMultiplier) +
            ((this.Strength * 2) + this.Agility);

        public int MaxDamage =>
            (this.weaponTypeable.MaxDamage * this.weaponLevelable.DamageMultiplier) +
            ((this.Strength * 3) + (this.Agility * 4));

        public int Strength =>
            this.gems.Where(g => g != null).Sum(g => g.Strength);

        public int Agility =>
            this.gems.Where(g => g != null).Sum(g => g.Agility);

        public int Vitality =>
            this.gems.Where(g => g != null).Sum(g => g.Vitality);

        public int SocketCount { get; }

        public void AddGem(IGem gem, int index)
        {
            if (index < this.gems.Length && this.gems[index] == null)
            {
                this.gems[index] = gem;
            }
        }

        public void RemoveGem(int index)
        {
            if (index < this.gems.Length && this.gems[index] != null)
            {
                this.gems[index] = null;
            }
        }

        public override string ToString()
        {
            return $"{this.MinDamage}-{this.MaxDamage} Damage, +{this.Strength} Strength, +{this.Agility} Agility, +{this.Vitality} Vitality";
        }
    }
}