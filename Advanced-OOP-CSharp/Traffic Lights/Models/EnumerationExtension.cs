namespace P09TrafficLights.Models
{
    using System;
    using Enums;

    public static class EnumerationExtension
    {
        public static int SizeOfEnumeration(this Type enumType)
        {
            int size = Enum.GetNames(enumType).Length;
            return size;
        }

        public static Type TypeOfEnum(this ValueType enumValueType)
        {
            return enumValueType.GetType();
        }

        public static int GetIndexOfEnum(this TrafficLightEnumeration current)
        {
            return (int)current;
        }

        public static TrafficLightEnumeration GetStringValueAsEnumType(
            this Type enumType, string value)
        {
            return (TrafficLightEnumeration)Enum.Parse(enumType, value);
        }

        public static string GetValueOfIndex(this Type enumType, int index)
        {
            return Enum.GetNames(enumType)[index];
        }

        public static bool IsIndexOutOfBound(this Type enumType, int index)
        {
            return index >= enumType.SizeOfEnumeration();
        }
    }
}