using System;
using NUnit.Framework;

[TestFixture]
class EntpTests25
{
    [Test]
    public void GetByPositionThrowException()
    {
        IEnterprise enterprise = new Enterprise();
        Employee employee = new Employee("pesho", "123", 777, Position.Hr, DateTime.Now);
        Employee employee1 = new Employee("a", "321", 777, Position.Owner, DateTime.Now);
        Employee employee2 = new Employee("c", "111", 777, Position.Hr, DateTime.Now);
        Employee employee3 = new Employee("b", "11111", 777, Position.Developer, DateTime.Now);


        Assert.Throws<ArgumentException>(() => enterprise.GetByPosition(Position.TeamLead));
    }
}
