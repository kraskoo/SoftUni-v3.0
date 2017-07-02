using System;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
class EntpTests44
{
    [Test]
    public void SearchAllWithPositionAndMinSalaryReturnNothing()
    {
        IEnterprise enterprise = new Enterprise();
        Employee employee = new Employee("pesho", "123", 62342, Position.Hr, DateTime.Now);
        Employee employee1 = new Employee("a", "321", 51255, Position.Owner, DateTime.Now);
        Employee employee2 = new Employee("c", "111", 11266, Position.Hr, DateTime.Now);
        Employee employee3 = new Employee("d", "11111", 1156123, Position.Developer, DateTime.Now);
        Employee employee4 = new Employee("e", "11111", 126126, Position.Developer, DateTime.Now);

        Employee[] employees = new Employee[]{
            employee,
            employee1,
            employee2,
            employee3,
            employee4
        };

        foreach (Employee e in employees)
        {
            enterprise.Add(e);
        }

        IEnumerable<Employee> all = enterprise.AllWithPositionAndMinSalary(Position.Manager, 1);
        foreach (Employee e in all)
        {
            Assert.Fail();
        }
    }
}
