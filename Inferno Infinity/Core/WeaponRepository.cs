namespace P10InfernoInfinity.Core
{
    using System.Collections.Generic;
    using Factories;
    using Interfaces;
    using Interfaces.Factories;
    using Models.Gems;
    using Models.Weapons;

    public class WeaponRepository : IWeaponRepository
    {
        private readonly Dictionary<string, IWeapon> weaponsByName;
        private readonly IGemTypeFactory gemTypeFactory;
        private readonly IGemLevelFactory gemLevelFactory;
        private readonly IWeaponTypeFactory weaponTypeFactory;
        private readonly IWeaponLevelFactory weaponLevelFactory;

        public WeaponRepository(
            IGemTypeFactory gemTypeFactory,
            IGemLevelFactory gemLevelFactory,
            IWeaponTypeFactory weaponTypeFactory,
            IWeaponLevelFactory weaponLevelFactory,
            Dictionary<string, IWeapon> weaponsByName)
        {
            this.gemTypeFactory = gemTypeFactory;
            this.gemLevelFactory = gemLevelFactory;
            this.weaponTypeFactory = weaponTypeFactory;
            this.weaponLevelFactory = weaponLevelFactory;
            this.weaponsByName = weaponsByName;
        }

        public WeaponRepository() : this(
            new GemTypeFactory(),
            new GemLevelFactory(),
            new WeaponTypeFactory(),
            new WeaponLevelFactory(), 
            new Dictionary<string, IWeapon>())
        {
        }

        public void CreateWeapon(string weaponType, string weaponName)
        {
            if (!this.weaponsByName.ContainsKey(weaponName))
            {
                string[] weaponTypeData =
                    weaponType.Split(' ');
                IWeaponTypeable newWeaponType =
                    this.weaponTypeFactory.CreateWeaponType(weaponTypeData[1]);
                IWeaponLevelable newWeaponLevel =
                    this.weaponLevelFactory.CreateWeaponLevel(weaponTypeData[0]);
                IWeapon newWeapon = new Weapon(newWeaponType, newWeaponLevel);
                this.weaponsByName.Add(weaponName, newWeapon);
            }
        }

        public void AddGemToWeapon(string weaponName, int socketIndex, string gemType)
        {
            if (this.weaponsByName.ContainsKey(weaponName))
            {
                string[] gemTypeData =
                    gemType.Split(' ');
                IGemTypeable newGemType =
                    this.gemTypeFactory.CreateGemType(gemTypeData[1]);
                IGemLevelable newGemLevel =
                    this.gemLevelFactory.CreateGemLevel(gemTypeData[0]);
                IGem newGem = new Gem(newGemType, newGemLevel);
                this.weaponsByName[weaponName].AddGem(newGem, socketIndex);
            }
        }

        public void RemoveGemFromWeapon(string weaponName, int socketIndex)
        {
            if (this.weaponsByName.ContainsKey(weaponName))
            {
                this.weaponsByName[weaponName].RemoveGem(socketIndex);
            }
        }

        public string Print(string weaponName)
        {
            return $"{weaponName}: {this.weaponsByName[weaponName]}";
        }
    }
}