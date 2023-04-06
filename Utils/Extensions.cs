using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroneSimulation.Utils
{
    public static class StringExtensions
    {
        public static string RemoverStrs(this string str, string[] removeStrs)
        {
            foreach (var removeStr in removeStrs)
                str = str.Replace(removeStr, "");
            return str;
        }
    }

    public static class EnumExtensions
    {
        public static Enum GetRandomEnumValue(this Type t)
        {
            return Enum.GetValues(t)          // get values from Type provided
                .OfType<Enum>()               // casts to Enum
                .OrderBy(e => Guid.NewGuid()) // mess with order of results
                .FirstOrDefault();            // take first item in result
        }
    }
}
