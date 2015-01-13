using System;
using System.Collections.Generic;

namespace Fenton.Selenium.SuperDriver
{
    [Serializable]
    public class MismatchBetweenBrowsersException<T> : SuperException
    {
        public MismatchBetweenBrowsersException(IEnumerable<T> values) : base(string.Format("Not all elements match: {0}", string.Join(",", values))) { }
    }
}
