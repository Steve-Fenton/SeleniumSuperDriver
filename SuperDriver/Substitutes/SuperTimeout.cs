using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperTimeout : ITimeouts
    {
        private readonly IEnumerable<ITimeouts> _timeouts;

        public SuperTimeout(IEnumerable<ITimeouts> timeouts) {
            _timeouts = timeouts;
        }

        public ITimeouts ImplicitlyWait(TimeSpan timeToWait)
        {
            return new SuperTimeout(_timeouts.AsParallel().Select(t => t.ImplicitlyWait(timeToWait)).ToList());
        }

        public ITimeouts SetPageLoadTimeout(TimeSpan timeToWait)
        {
            return new SuperTimeout(_timeouts.AsParallel().Select(t => t.SetPageLoadTimeout(timeToWait)).ToList());
        }

        public ITimeouts SetScriptTimeout(TimeSpan timeToWait)
        {
            return new SuperTimeout(_timeouts.AsParallel().Select(t => t.SetScriptTimeout(timeToWait)).ToList());
        }
    }
}
