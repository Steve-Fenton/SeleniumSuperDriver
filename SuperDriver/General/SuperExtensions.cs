using System;
using System.Collections.Concurrent;
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

        public static IEnumerable<string> AssertLooseStringMatch(this IEnumerable<string> values)
        {
            var usableValues = values.Where(v => string.IsNullOrWhiteSpace(v) == false).Distinct().ToList();
            if (usableValues.Count() > 1)
            {
                throw new MismatchBetweenBrowsersException<string>(values);
            }

            return usableValues;
        }

        public static IEnumerable<DateTime?> AssertLooseDateMatch(this IEnumerable<DateTime?> values)
        {
            var usableValues = values.Where(v => v.HasValue).Distinct().ToList();

            if (usableValues.Count() > 1)
            {
                throw new MismatchBetweenBrowsersException<DateTime?>(values);
            }

            return usableValues;
        }

        public static ParallelQuery<T> ToConcurrentQuery<T>(this IEnumerable<T> values)
        {
            return new ConcurrentBag<T>(values.ToList()).AsParallel();
        }
    }
}
