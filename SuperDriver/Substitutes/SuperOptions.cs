using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperOptions : IOptions
    {
        private readonly ParallelQuery<IOptions> _query;

        public SuperOptions(IEnumerable<IOptions> options)
        {
            _query = options.ToConcurrentQuery();
        }

        public ICookieJar Cookies
        {
            get
            {
                return new SuperCookieJar(_query.Select(o => o.Cookies));
            }
        }

        public IWindow Window
        {
            get
            {
                return new SuperWindow(_query.Select(o => o.Window));
            }
        }

        public ITimeouts Timeouts()
        {
            return new SuperTimeouts(_query.Select(o => o.Timeouts()));
        }
    }
}
