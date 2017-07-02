using System;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
class EntpTests41
{
    [Test]
    public void SearchByNameAndPositionEmptyCollection()
    {
        IEnterprise enterprise = new Enterprise();
        Employee employee = new Employee("a", "123", 62342, Position.Hr, DateTime.Now);
        Employee employee1 = new Employee("b", "321", 51255, Position.Owner, DateTime.Now);
        Employee employee2 = new Employee("c", "111", 11266, Position.Hr, DateTime.Now);
        Employee employee3 = new Employee("d", "11111", 1156123, Position.Developer, DateTime.Now);
        Employee employee4 = new Employee("e", "11111", 126126, Position.Developer, DateTime.Now);

        Employee[] employees = new Employee[]{
            employee1,
            employee2,
            employee3,
            employee4,
            employee,
        };

        foreach (Employee employee5 in employees)
        {
            enterprise.Add(employee5);
        }

        IEnumerable<Employee> employees1 = enterprise.SearchByNameAndPosition("gosho", "pesho", Position.TeamLead);
        foreach (Employee employee5 in employees1)
        {
            Assert.Fail();
        }
    }
}