using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Enterprise : IEnterprise
{
    private readonly Dictionary<Guid, Employee> employeeById;
    private readonly Dictionary<Position, HashSet<Employee>> employeeByPosition;
    private readonly OrderedDictionary<double, HashSet<Employee>> employeeBySalary;

    public Enterprise()
    {
        this.employeeById = new Dictionary<Guid, Employee>();
        this.employeeByPosition = new Dictionary<Position, HashSet<Employee>>();
        this.employeeBySalary = new OrderedDictionary<double, HashSet<Employee>>();
    }

    public int Count => this.employeeById.Count;

    public void Add(Employee employee)
    {
        this.employeeById.Add(employee.Id, employee);
        this.AddEmployeeByPosition(employee);
        this.AddEmployeeBySalary(employee);
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        if (!this.employeeBySalary.ContainsKey(minSalary) || !this.employeeByPosition.ContainsKey(position))
        {
            return Enumerable.Empty<Employee>();
        }

        var minSalaryEmployees = this.employeeBySalary[minSalary];
        return minSalaryEmployees.Intersect(this.employeeByPosition[position]);
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (!this.employeeById.ContainsKey(guid))
        {
            return false;
        }

        var currentEmployee = this.employeeById[guid];
        this.employeeByPosition[currentEmployee.Position].Remove(currentEmployee);
        this.employeeBySalary[currentEmployee.Salary].Remove(currentEmployee);
        this.employeeById[guid].FirstName = employee.FirstName;
        this.employeeById[guid].LastName = employee.LastName;
        this.employeeById[guid].Position = employee.Position;
        this.employeeById[guid].HireDate = employee.HireDate;
        this.employeeById[guid].Salary = employee.Salary;
        this.AddEmployeeByPosition(this.employeeById[guid]);
        this.AddEmployeeBySalary(this.employeeById[guid]);
        return true;
    }

    public bool Contains(Guid guid)
    {
        return this.employeeById.ContainsKey(guid);
    }

    public bool Contains(Employee employee)
    {
        return this.employeeById.ContainsKey(employee.Id);
    }

    public bool Fire(Guid guid)
    {
        if (!this.employeeById.ContainsKey(guid))
        {
            return false;
        }

        var employeeToFire = this.employeeById[guid];
        this.employeeByPosition[employeeToFire.Position].Remove(employeeToFire);
        this.employeeBySalary[employeeToFire.Salary].Remove(employeeToFire);
        this.employeeById.Remove(guid);
        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!this.employeeById.ContainsKey(guid))
        {
            throw new ArgumentException();
        }

        return this.employeeById[guid];
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

        return this.GetBySalary(salary).Intersect(this.employeeByPosition[position]);
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
        if (!this.employeeById.ContainsKey(guid))
        {
            throw new InvalidOperationException();
        }

        return this.employeeById[guid].Position;
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
            this.employeeById.Remove(employee.Id);
            this.employeeByPosition[employee.Position].Remove(employee);
            this.employeeBySalary[employee.Salary].Remove(employee);
            var percentageUp = $".{percent}";
            percentageUp = percent <= 100 ? $"1{percentageUp}" : $"{percent / 100}{percentageUp}";
            var percentageMultiplier = double.Parse(percentageUp);
            var risedSalary = employee.Salary * percentageMultiplier;
            employee.Salary = risedSalary;
            this.employeeById.Add(employee.Id, employee);
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
                .Where(empl => empl.Value.LastName.Equals(lastName) &&
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
            this.employeeBySalary.Add(employee.Salary, new HashSet<Employee>());
        }

        this.employeeBySalary[employee.Salary].Add(employee);
    }

    private void AddEmployeeByPosition(Employee employee)
    {
        if (!this.employeeByPosition.ContainsKey(employee.Position))
        {
            this.employeeByPosition.Add(employee.Position, new HashSet<Employee>());
        }

        this.employeeByPosition[employee.Position].Add(employee);
    }
}

