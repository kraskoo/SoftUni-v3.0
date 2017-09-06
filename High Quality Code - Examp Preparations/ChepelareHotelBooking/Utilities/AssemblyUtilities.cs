namespace HotelBookingSystem.Utilities
{
    using System.Collections.Generic;
    using System.Reflection;

    public class AssemblyUtilities
    {
        public static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        public static readonly IEnumerable<TypeInfo> Types = CurrentAssembly.DefinedTypes;
    }
}