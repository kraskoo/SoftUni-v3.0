using System.Collections.Generic;

public class EmployeeSalaryEqualityComparer : IEqualityComparer<Employee>
{
    public bool Equals(Employee x, Employee y)
    {
        return x.Id.Equals(y.Id) && x.Salary.Equals(y.Salary);
    }

    public int GetHashCode(Employee obj)
    {
        return obj.Id.GetHashCode() ^ obj.Salary.GetHashCode();
    }
}