using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

[TestFixture]
class EntpTests19
{
    [Test]
    public void InsertEmployees_RaiseSalaries_ShouldWorkCorrectly()
    {
        IEnterprise enterprise = new Enterprise();

        DateTime calendar = new DateTime(2017, 1, 1);
        Employee employee = new Employee("pesho", "123", 777, Position.Hr, calendar);
        Employee employee1 = new Employee("a", "321", 777, Position.Owner, calendar);
        Employee employee2 = new Employee("c", "111", 777, Position.Hr, calendar);
        Employee employee3 = new Employee("b", "11111", 777, Position.Developer, calendar);

        enterprise.Add(employee);
        enterprise.Add(employee1);
        enterprise.Add(employee2);
        enterprise.Add(employee3);

        bool b = enterprise.RaiseSalary(1, 50);
        Assert.True(b);

        IEnumerable<Employee> bySalary = enterprise.GetBySalary(0);

        int size = 0;
        foreach (Employee employee4 in bySalary)
        {
            size++;
        }

        Assert.True(size == 4);
    }
}
