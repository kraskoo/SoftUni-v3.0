using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Enterprise : IEnterprise
{
    private readonly Dictionary<string, Employee> employeeById;
    private readonly Dictionary<Position, HashSet<Employee>> employeeByPosition;
    private readonly OrderedDictionary<double, HashSet<Employee>> employeeBySalary;

    public Enterprise()
    {
        this.employeeById = new Dictionary<string, Employee>();
        this.employeeByPosition = new Dictionary<Position, HashSet<Employee>>();
        this.employeeBySalary = new OrderedDictionary<double, HashSet<Employee>>();
    }

    public int Count => this.employeeById.Count;

    public void Add(Employee employee)
    {
        this.employeeById.Add(employee.Id.ToString(), employee);
        this.AddEmployeeByPosition(employee);
        this.AddEmployeeBySalary(employee);
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        if (!this.employeeBySalary.ContainsKey(minSalary) || !this.employeeByPosition.ContainsKey(position))
        {
            return Enumerable.Empty<Employee>();
        }

        return this.GetByPosition(position).Intersect(this.GetBySalary(minSalary), new EmplyeePositionAndSalaryEqualityComparer());
    }

    public bool Change(Guid guid, Employee employee)
    {
        var strGuid = guid.ToString();
        if (!this.employeeById.ContainsKey(strGuid))
        {
            return false;
        }

        var currentEmployee = this.employeeById[strGuid];
        this.employeeByPosition[currentEmployee.Position].Remove(currentEmployee);
        this.employeeBySalary[currentEmployee.Salary].Remove(currentEmployee);
        this.employeeById[strGuid].FirstName = employee.FirstName;
        this.employeeById[strGuid].LastName = employee.LastName;
        this.employeeById[strGuid].Position = employee.Position;
        this.employeeById[strGuid].HireDate = employee.HireDate;
        this.employeeById[strGuid].Salary = employee.Salary;
        this.AddEmployeeByPosition(this.employeeById[strGuid]);
        this.AddEmployeeBySalary(this.employeeById[strGuid]);
        return true;
    }

    public bool Contains(Guid guid)
    {
        return this.employeeById.ContainsKey(guid.ToString());
    }

    public bool Contains(Employee employee)
    {
        return this.employeeById.ContainsKey(employee.Id.ToString());
    }

    public bool Fire(Guid guid)
    {
        var strGuid = guid.ToString();
        if (!this.employeeById.ContainsKey(strGuid))
        {
            return false;
        }

        var employeeToFire = this.employeeById[strGuid];
        this.employeeByPosition[employeeToFire.Position].Remove(employeeToFire);
        this.employeeBySalary[employeeToFire.Salary].Remove(employeeToFire);
        this.employeeById.Remove(strGuid);
        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        var strGuid = guid.ToString();
        if (!this.employeeById.ContainsKey(strGuid))
        {
            throw new ArgumentException();
        }

        return this.employeeById[strGuid];
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        if (!this.employeeByPosition.ContainsKey(position))
        {
            throw new ArgumentException();
        }

        return this.employeeByPosition[position];
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        var salaryInRange = this.employeeBySalary.RangeFrom(minSalary, true);
        if (salaryInRange.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return salaryInRange.SelectMany(e => e.Value);
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        if (!this.employeeBySalary.ContainsKey(salary) ||
            !this.employeeByPosition.ContainsKey(position))
        {
            throw new InvalidOperationException();
        }

        return this.employeeByPosition[position].Intersect(this.employeeBySalary[salary]);
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        foreach (var employee in this.employeeById)
        {
            yield return employee.Value;
        }
    }

    public Position PositionByGuid(Guid guid)
    {
        var strGuid = guid.ToString();
        if (!this.employeeById.ContainsKey(strGuid))
        {
            throw new InvalidOperationException();
        }

        return this.employeeById[strGuid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        var currentDate = DateTime.Now;
        var matchEmployees = this.employeeById
            .Where(empl => empl.Value.HireDate.AddMonths(months).Date <= currentDate)
            .Select(empl => empl.Value)
            .ToList();
        if (matchEmployees.Count == 0)
        {
            return false;
        }

        foreach (var employee in matchEmployees)
        {
            var strGuid = employee.Id.ToString();
            this.employeeById.Remove(strGuid);
            this.employeeByPosition[employee.Position].Remove(employee);
            this.employeeBySalary[employee.Salary].Remove(employee);
            var percentageMultiplier = double.Parse($"{percent / 100}");
            var risedSalary = employee.Salary * percentageMultiplier;
            employee.Salary = risedSalary;
            this.employeeById.Add(strGuid, employee);
            this.AddEmployeeByPosition(employee);
            this.AddEmployeeBySalary(employee);
        }

        return true;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        return this.employeeById
            .Where(empl => empl.Value.FirstName.Equals(firstName))
            .Select(empl => empl.Value);
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        return this.employeeById
                .Where(empl => empl.Value.FirstName.Equals(firstName) &&
                               empl.Value.LastName.Equals(lastName) &&
                               empl.Value.Position.Equals(position))
                .Select(empl => empl.Value);
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        var intersectedPositions = this.employeeByPosition.Keys.Intersect(positions);
        foreach (var position in intersectedPositions)
        {
            var employeesOnPosition = this.employeeByPosition[position];
            foreach (var employee in employeesOnPosition)
            {
                yield return employee;
            }
        }
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        return this.employeeBySalary
            .Range(minSalary, true, maxSalary, true)
            .SelectMany(e => e.Value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private void AddEmployeeBySalary(Employee employee)
    {
        if (!this.employeeBySalary.ContainsKey(employee.Salary))
        {
            this.employeeBySalary.Add(
                employee.Salary,
                new HashSet<Employee>(
                    new EmployeeSalaryEqualityComparer()));
        }

        this.employeeBySalary[employee.Salary].Add(employee);
    }

    private void AddEmployeeByPosition(Employee employee)
    {
        if (!this.employeeByPosition.ContainsKey(employee.Position))
        {
            this.employeeByPosition.Add(
                employee.Position,
                new HashSet<Employee>(
                    new EmployeePositionEqualityComparer()));
        }

        this.employeeByPosition[employee.Position].Add(employee);
    }
}

