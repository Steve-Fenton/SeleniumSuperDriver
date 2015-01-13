using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperOptions : IOptions
    {
        private readonly IEnumerable<IOptions> _options;

        public SuperOptions(IEnumerable<IOptions> options)
        {
            _options = options;
        }

        public ICookieJar Cookies
        {
            get
            {
                return new SuperCookieJar(_options.AsParallel().Select(o => o.Cookies).ToList());
            }
        }

        public IWindow Window
        {
            get
            {
                return new SuperWindow(_options.AsParallel().Select(o => o.Window).ToList());
            }
        }

        public ITimeouts Timeouts()
        {
            return new SuperTimeouts(_options.AsParallel().Select(o => o.Timeouts()).ToList());
        }
    }
}
