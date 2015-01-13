using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public static class SuperExtensions
    {
        public static IEnumerable<T> AssertAllMatch<T>(this IEnumerable<T> values)
        {
            if (values.Distinct().Count() > 1)
            {
                throw new MismatchBetweenBrowsersException<T>(values);
            }

            return values;
        }
    }
}
