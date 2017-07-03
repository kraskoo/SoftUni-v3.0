using System.Collections.Generic;

public class EmployeePositionEqualityComparer : IEqualityComparer<Employee>
{
    public bool Equals(Employee x, Employee y)
    {
        return x.Id.Equals(y.Id) && x.Position.Equals(y.Position);
    }

    public int GetHashCode(Employee obj)
    {
        return obj.Id.GetHashCode() ^ obj.Position.GetHashCode();
    }
}