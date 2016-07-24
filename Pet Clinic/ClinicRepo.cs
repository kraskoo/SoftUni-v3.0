namespace P08PetClinic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ClinicRepo
    {
        private readonly IDictionary<string, Clinic> clinics;
        private readonly ISet<Pet> pets;

        public ClinicRepo(
            IDictionary<string, Clinic> clinics,
            ISet<Pet> pets)
        {
            this.clinics = clinics;
            this.pets = pets;
        }

        public ClinicRepo() : this(
            new Dictionary<string, Clinic>(),
            new HashSet<Pet>())
        {
        }

        public bool HasEmpty(string clinicName)
        {
            if (!this.clinics.ContainsKey(clinicName))
            {
                throw new ArgumentException("Invalid Operation!");
            }

            return this.clinics[clinicName].HasEmptyRoom();
        }

        public bool AddPetToClinic(string clinicName, string petName)
        {
            if (this.GetClinicByName(clinicName) == null)
            {
                throw new ArgumentException("Invalid Operation!");
            }

            if (this.GetPetByName(petName) == null)
            {
                throw new ArgumentException("Invalid Operation!");
            }

            bool hasAddPet =
                this.GetClinicByName(clinicName)
                    .AddNewPetToRoom(this.GetPetByName(petName));
            if (hasAddPet)
            {
                return true;
            }

            return false;
        }

        public void AddClinic(string name, int rooms)
        {
            if (!this.clinics.ContainsKey(name))
            {
                this.clinics.Add(name, new Clinic(rooms));
            }
        }

        public void AddPet(string name, int age, string kind)
        {
            var newPet = new Pet(name, age, kind);
            this.pets.Add(newPet);
        }

        public bool Release(string name)
        {
            if (this.clinics.ContainsKey(name))
            {
                this.clinics.Remove(name);
                return true;
            }

            return false;
        }

        public Clinic GetClinicByName(string name)
        {
            if (!this.clinics.ContainsKey(name))
            {
                return null;
            }

            return this.clinics[name];
        }

        public Pet GetPetByName(string name)
        {
            return this.pets.FirstOrDefault(p => p.Name.Equals(name));
        }
    }
}