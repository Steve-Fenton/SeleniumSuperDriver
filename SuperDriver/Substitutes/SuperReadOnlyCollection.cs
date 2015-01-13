using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperReadOnlyCollection<T> : ReadOnlyCollection<T>
    {
        private readonly IEnumerable<ReadOnlyCollection<T>> _collections;

        public SuperReadOnlyCollection(IEnumerable<ReadOnlyCollection<T>> collections)
            :base(new List<T>())
        {
            _collections = collections;
        }

        new public int Count
        {
            get
            {
                return _collections.AsParallel().Select(c => c.Count).AssertAllMatch().FirstOrDefault();
            }
        }

        new public T this[int index]
        {
            get
            {
                return _collections.AsParallel().Select(c => c[index]).AssertAllMatch().FirstOrDefault();
            }
        }

        new public bool Contains(T value)
        {
            return _collections.AsParallel().Select(c => c.Contains(value)).AssertAllMatch().FirstOrDefault();
        }

        new public void CopyTo(T[] array, int index)
        {
            throw new NotImplementedException();
        }

        new public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        new public int IndexOf(T value)
        {
            return _collections.AsParallel().Select(c => c.IndexOf(value)).AssertAllMatch().FirstOrDefault();
        }
    }
}
