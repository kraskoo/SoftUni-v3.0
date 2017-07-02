using System;
using NUnit.Framework;

[TestFixture]
class EntpTests12
{
    [Test]
    public void ChangeGuidExistingEmployee()
    {
        IEnterprise enterprise = new Enterprise();

        Employee employee = new Employee("pesho", "gosho", 123, Position.Hr, DateTime.Now);
        enterprise.Add(employee);

        Employee toReplace = new Employee("replaced", "replacedEmployee", 555, Position.Owner, DateTime.Now);
        enterprise.Add(new Employee("sosho", "pesho", 321, Position.Manager, DateTime.Now));
        enterprise.Add(new Employee("posho", "kosho", 55, Position.Hr, DateTime.Now));
        enterprise.Add(new Employee("gosho", "mosho", 1, Position.Manager, DateTime.Now));

        Assert.True(enterprise.Change(employee.Id, toReplace));
        Assert.True(enterprise.Contains(employee.Id));
    }
}
