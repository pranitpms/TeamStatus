using System;

namespace EntityObject.Utility
{
    public class NullValue
    {
        public static bool IsNullableType(Type typeToCheck)
        {
            if (Nullable.GetUnderlyingType(typeToCheck) != null)
                return true;

            if (typeToCheck == typeof(string))
                return true;

            if (typeToCheck == typeof(object))
                return true;

            return false;
        }

        public static DateTime DateTimeValue()
        {
            var dateTime = DateTime.MinValue;

            return (dateTime);
        }
    }
}
