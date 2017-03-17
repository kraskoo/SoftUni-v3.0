namespace SandBoxes.MapperSandbox.Mapped
{
    public static class MappedTypeExtensions
    {
        public static PersonDto GetMappedPersonDto(this Person person)
        {
            return person.GetMapped(p => new PersonDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName
            });
        }
    }
}