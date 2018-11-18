using System;
using System.Linq;

namespace CommandScheduler.Utilities
{
    public class Guard
    {
        public static void ForNull(object value)
        {
            if (value == null)
                throw new ArgumentNullException($"{value.GetType().Name} cannot be null");
        }

        public static void ForLessEqualZero(int value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(parameterName);
        }

        public static void ForNullOrEmpty(string value, string parameterName)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentOutOfRangeException(parameterName);
        }

        public static void ForType(object value, Type expected)
        {
            //TODO
            throw new ArrayTypeMismatchException("Expected type is " + expected.Name);
        }
    }
}
