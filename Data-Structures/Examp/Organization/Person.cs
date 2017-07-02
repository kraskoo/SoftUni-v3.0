using System;

public class Person : IComparable<Person>
{
    public Person(string name, double salary)
    {
        this.Name = name;
        this.Salary = salary;
    }

    public string Name { get; set; }

    public double Salary { get; set; }

    public int CompareTo(Person other)
    {
        var nameCmp = string.Compare(this.Name, other.Name, StringComparison.CurrentCulture);
        if (nameCmp == 0)
        {
            return this.Salary.CompareTo(other.Salary);
        }
        return nameCmp;
    }

    public override string ToString()
    {
        return $"{this.Name} {this.Salary}";
    }
}