namespace P08PetClinic
{
    using System;
    using System.Collections.Generic;

    public class PetComparer : IComparer<Pet>
    {
        public int Compare(Pet x, Pet y)
        {
            return
                string
                    .Compare(
                    $"{x.Name}{x.Age}{x.Kind}",
                    $"{y.Name}{y.Age}{y.Kind}",
                    StringComparison.Ordinal);
        }
    }
}