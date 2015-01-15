using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperReadOnlyCollection
    {
        public static ReadOnlyCollection<T> MergeCollections<T, TSubstitute>(ParallelQuery<ReadOnlyCollection<T>> collections) where TSubstitute: T
        {
            var mergedCollection = new ConcurrentBag<T>();

            // Make sure they all have the same number of elements
            var count = collections.Select(c => c.Count).AssertAllMatch().First();

            for (var elementIndex = 0; elementIndex < count; elementIndex++)
            {
                IList<T> elementFromEachCollection = new List<T>();
                for (var collectionIndex = 0; collectionIndex < collections.Count(); collectionIndex++)
                {
                    elementFromEachCollection.Add(collections.ElementAt(collectionIndex).ElementAt(elementIndex));
                }

                var instance = (TSubstitute)Activator.CreateInstance(typeof(TSubstitute), elementFromEachCollection);
                mergedCollection.Add(instance);
            }

            return new ReadOnlyCollection<T>(mergedCollection.ToList());
        }
    }
}
