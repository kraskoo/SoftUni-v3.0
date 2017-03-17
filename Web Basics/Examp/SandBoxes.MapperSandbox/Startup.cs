namespace SandBoxes.MapperSandbox
{
    using System;
    using Mapped;

    public class Startup
    {
        public static void Main()
        {
            var person = new Person
            {
                FirstName = "Pesho",
                LastName = "Petrov",
                Age = 17
            };

            var personDto = person.GetMappedPersonDto();
            Console.WriteLine($"{personDto.FirstName}{personDto.LastName}");
        }
    }
}