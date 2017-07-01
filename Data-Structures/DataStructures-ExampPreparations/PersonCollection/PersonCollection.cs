using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private readonly Dictionary<string, Person> personByEmail =
        new Dictionary<string, Person>();
    private readonly Dictionary<string, SortedSet<Person>> personByDomain =
        new Dictionary<string, SortedSet<Person>>();
    private readonly Dictionary<string, SortedSet<Person>> personByNameAndTown =
        new Dictionary<string, SortedSet<Person>>();
    private readonly OrderedDictionary<int, SortedSet<Person>> personByAge =
        new OrderedDictionary<int, SortedSet<Person>>();
    private readonly Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> personByTownAndAge =
        new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();

    public bool AddPerson(string email, string name, int age, string town)
    {
        var newPerson = new Person(email, name, age, town);
        bool isContainPerson;
        this.AddPersonByEmail(email, newPerson, out isContainPerson);
        if (!isContainPerson)
        {
            return false;
        }

        this.AddPersonByDomain(email, newPerson);
        this.AddPersonByNameAndTown(name, town, newPerson);
        this.AddPersonByAge(age, newPerson);
        this.AddPersonByTownAndAge(age, town, newPerson);
        return true;
    }

    public int Count => this.personByEmail.Count;

    public Person FindPerson(string email)
    {
        if (!this.personByEmail.ContainsKey(email))
        {
            return null;
        }

        return this.personByEmail[email];
    }

    public bool DeletePerson(string email)
    {
        if (!this.personByEmail.ContainsKey(email))
        {
            return false;
        }

        var person = this.personByEmail[email];
        this.personByEmail.Remove(email);
        this.personByDomain[this.GetDomain(email)].Remove(person);
        this.personByNameAndTown[this.GetNameAndTownKey(person.Name, person.Town)].Remove(person);
        this.personByAge[person.Age].Remove(person);
        this.personByTownAndAge[person.Town][person.Age].Remove(person);
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (!this.personByDomain.ContainsKey(emailDomain))
        {
            return Enumerable.Empty<Person>();
        }

        return this.personByDomain[emailDomain];
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var key = this.GetNameAndTownKey(name, town);
        if (!this.personByNameAndTown.ContainsKey(key))
        {
            return Enumerable.Empty<Person>();
        }

        return this.personByNameAndTown[key];
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var personInRange = this.personByAge.Range(startAge, true, endAge, true);
        if (!personInRange.Any())
        {
            return Enumerable.Empty<Person>();
        }

        return personInRange.SelectMany(p => p.Value);
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (!this.personByTownAndAge.ContainsKey(town))
        {
            return Enumerable.Empty<Person>();
        }

        var personInRange = this.personByTownAndAge[town].Range(startAge, true, endAge, true);
        if (!personInRange.Any())
        {
            return Enumerable.Empty<Person>();
        }

        return personInRange.SelectMany(p => p.Value);
    }

    private void AddPersonByNameAndTown(string name, string town, Person newPerson)
    {
        var nameAndTownKey = this.GetNameAndTownKey(name, town);
        if (!this.personByNameAndTown.ContainsKey(nameAndTownKey))
        {
            this.personByNameAndTown.Add(nameAndTownKey, new SortedSet<Person>());
        }

        this.personByNameAndTown[nameAndTownKey].Add(newPerson);
    }

    private void AddPersonByDomain(string email, Person newPerson)
    {
        var domain = this.GetDomain(email);
        if (!this.personByDomain.ContainsKey(domain))
        {
            this.personByDomain.Add(domain, new SortedSet<Person>());
        }

        this.personByDomain[domain].Add(newPerson);
    }

    private void AddPersonByEmail(string email, Person newPerson, out bool isContainPerson)
    {
        isContainPerson = false;
        if (!personByEmail.ContainsKey(email))
        {
            personByEmail.Add(email, newPerson);
            isContainPerson = true;
        }
    }

    private void AddPersonByAge(int age, Person newPerson)
    {
        if (!this.personByAge.ContainsKey(age))
        {
            this.personByAge.Add(age, new SortedSet<Person>());
        }

        this.personByAge[age].Add(newPerson);
    }

    private void AddPersonByTownAndAge(int age, string town, Person newPerson)
    {
        if (!this.personByTownAndAge.ContainsKey(town))
        {
            this.personByTownAndAge.Add(town, new OrderedDictionary<int, SortedSet<Person>>());
        }

        if (!this.personByTownAndAge[town].ContainsKey(age))
        {
            this.personByTownAndAge[town].Add(age, new SortedSet<Person>());
        }

        this.personByTownAndAge[town][age].Add(newPerson);
    }

    private string GetNameAndTownKey(string name, string town)
    {
        return $"{name}{town}";
    }

    private string GetDomain(string email)
    {
        var domainIndex = email.IndexOf('@') + 1;
        return email.Substring(domainIndex);
    }
}
