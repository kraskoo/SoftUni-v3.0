using System;

public class Program
{
    static void Main(string[] args)
    {
        LinkedList<int> list = new LinkedList<int>();
        list.AddFirst(1);
        list.AddLast(2);
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}