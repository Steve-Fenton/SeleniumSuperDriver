using System;

namespace WebApplicationTests
{
    public static class StringEnumExtensions
    {
        public static T AsEnum<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Can't parse an empty string");
            }

            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("Type is not enumeration " + enumType.Name);
            }

            return (T)Enum.Parse(enumType, value, true);
        }
    }
}
