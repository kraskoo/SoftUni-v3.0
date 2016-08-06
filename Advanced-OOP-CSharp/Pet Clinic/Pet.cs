namespace P08PetClinic
{
    using System;

    public class Pet : IComparable<Pet>
    {
        public Pet(string name, int age, string kind)
        {
            this.Name = name;
            this.Age = age;
            this.Kind = kind;
        }

        public string Name { get; }

        public int Age { get; }

        public string Kind { get; }

        public override bool Equals(object obj)
        {
            Pet otherPet = obj as Pet;
            if (otherPet != null)
            {
                return this.CompareTo(otherPet) == 0;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return $"{this.Name}{this.Age}{this.Kind}".GetHashCode();
        }

        public int CompareTo(Pet other)
        {
            return
                string.Compare(
                    $"{this.Name}{this.Age}{this.Kind}",
                    $"{other.Name}{other.Age}{other.Kind}",
                    StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"{this.Name} {this.Age} {this.Kind}";
        }
    }
}