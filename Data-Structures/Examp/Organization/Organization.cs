using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Organization : IOrganization
{
    private readonly List<Person> personByIndex;
    private readonly MultiDictionary<string, Person> personByName;
    private readonly OrderedDictionary<int, List<Person>> personByNameSize;

    public Organization()
    {
        this.personByIndex = new List<Person>();
        this.personByName = new MultiDictionary<string, Person>(true);
        this.personByNameSize = new OrderedDictionary<int, List<Person>>();
    }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (var person in this.personByIndex)
        {
            yield return person;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count => this.personByIndex.Count;

    public bool Contains(Person person)
    {
        return this.personByName.ContainsKey(person.Name);
    }

    public bool ContainsByName(string name)
    {
        return this.personByName.ContainsKey(name);
    }

    public void Add(Person person)
    {
        this.personByIndex.Add(person);
        this.personByName.Add(person.Name, person);
        var nameLength = person.Name.Length;
        this.AddPersonByNameLength(person, nameLength);
    }

    public Person GetAtIndex(int index)
    {
        if (index > this.Count - 1 || index < 0)
        {
            throw new IndexOutOfRangeException();
        }

        return this.personByIndex[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        return this.personByName[name];
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        return this.personByIndex.Take(count);
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        return this.personByNameSize.Range(minLength, true, maxLength, true).SelectMany(p => p.Value);
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        if (!this.personByNameSize.ContainsKey(length))
        {
            throw new ArgumentException();
        }

        return this.personByNameSize[length];
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return this.personByIndex;
    }

    private void AddPersonByNameLength(Person person, int nameLength)
    {
        if (!this.personByNameSize.ContainsKey(nameLength))
        {
            this.personByNameSize.Add(nameLength, new List<Person>());
        }

        this.personByNameSize[nameLength].Add(person);
    }
}