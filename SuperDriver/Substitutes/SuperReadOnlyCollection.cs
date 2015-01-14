using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperReadOnlyCollection
    {
        //public static ReadOnlyCollection<IWebElement> MergeCollections(IEnumerable<ReadOnlyCollection<IWebElement>> collections)
        //{
        //    IList<IWebElement> elements = new List<IWebElement>();

        //    // Make sure they all have the same number of elements
        //    var count = collections.AsParallel().Select(c => c.Count).AssertAllMatch().First();

        //    for (var i = 0; i < count; i++)
        //    {
        //        IList<IWebElement> elementFromEachCollection = new List<IWebElement>();
        //        for (var j = 0; j < collections.Count(); j++) {
        //            elementFromEachCollection.Add(collections.ElementAt(j).ElementAt(i));
        //        }

        //        elements.Add(new SuperWebElement(elementFromEachCollection));
        //    }

        //    return new ReadOnlyCollection<IWebElement>(elements);
        //}

        public static ReadOnlyCollection<T> MergeCollections<T, TSubstitute>(IEnumerable<ReadOnlyCollection<T>> collections) where TSubstitute: T
        {
            IList<T> elements = new List<T>();

            // Make sure they all have the same number of elements
            var count = collections.AsParallel().Select(c => c.Count).AssertAllMatch().First();

            for (var i = 0; i < count; i++)
            {
                IList<T> elementFromEachCollection = new List<T>();
                for (var j = 0; j < collections.Count(); j++)
                {
                    elementFromEachCollection.Add(collections.ElementAt(j).ElementAt(i));
                }

                var x = (TSubstitute)Activator.CreateInstance(typeof(TSubstitute), elementFromEachCollection);
                elements.Add(x);
            }

            return new ReadOnlyCollection<T>(elements);
        }
    }
}
