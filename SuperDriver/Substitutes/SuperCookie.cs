using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperCookie : Cookie
    {
        public readonly ParallelQuery<Cookie> _query;

        public SuperCookie(IEnumerable<Cookie> cookies)
            : base(
                cookies.Select(c => c.Name).AssertAllMatch().First(),
                cookies.Select(c => c.Value).AssertAllMatch().First(),
                cookies.Select(c => c.Domain).AssertAllMatch().First(),
                cookies.Select(c => c.Path).AssertAllMatch().First(),
                cookies.Select(c => c.Expiry).AssertAllMatch().First())
        {
            _query = cookies.ToConcurrentQuery();
        }

        public override bool Secure
        {
            get
            {
                return _query.Select(c => c.Secure).AssertAllMatch().FirstOrDefault();
            }
        }
    }
}
