using System.Collections.Generic;

public class EmplyeePositionAndSalaryEqualityComparer : IEqualityComparer<Employee>
{
    public bool Equals(Employee x, Employee y)
    {
        return x.Id.Equals(y.Id) && x.Position.Equals(y.Position) && x.Salary.Equals(y.Salary);
    }

    public int GetHashCode(Employee obj)
    {
        return obj.Position.GetHashCode() ^ obj.Salary.GetHashCode();
    }
}