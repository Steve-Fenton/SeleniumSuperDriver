using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperTimeouts : ITimeouts
    {
        private readonly ParallelQuery<ITimeouts> _query;

        public SuperTimeouts(IEnumerable<ITimeouts> timeouts)
        {
            _query = timeouts.ToConcurrentQuery();
        }

        public ITimeouts ImplicitlyWait(TimeSpan timeToWait)
        {
            return new SuperTimeouts(_query.Select(t => t.ImplicitlyWait(timeToWait)));
        }

        public ITimeouts SetPageLoadTimeout(TimeSpan timeToWait)
        {
            return new SuperTimeouts(_query.Select(t => t.SetPageLoadTimeout(timeToWait)));
        }

        public ITimeouts SetScriptTimeout(TimeSpan timeToWait)
        {
            return new SuperTimeouts(_query.Select(t => t.SetScriptTimeout(timeToWait)));
        }
    }
}
