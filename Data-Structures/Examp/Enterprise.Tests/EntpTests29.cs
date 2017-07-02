using System;
using NUnit.Framework;

[TestFixture]
class EntpTests29
{
    [Test]
    public void getEmployeesBySalaryThrowException()
    {
        IEnterprise enterprise = new Enterprise();
        Employee employee = new Employee("pesho", "123", 777, Position.Hr, DateTime.Now);
        Employee employee1 = new Employee("a", "321", 123, Position.Owner, DateTime.Now);
        Employee employee2 = new Employee("c", "111", 1, Position.Hr, DateTime.Now);
        Employee employee3 = new Employee("d", "11111", 231, Position.Developer, DateTime.Now);

        enterprise.Add(employee);
        enterprise.Add(employee1);
        enterprise.Add(employee2);
        enterprise.Add(employee3);

        Employee[] employees = new Employee[] {
            employee1,
            employee2,
            employee3,
            employee
        };


        Assert.Throws<InvalidOperationException>(() => enterprise.GetBySalary(1000000));
    }
}