using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperCookie : Cookie
    {
        public readonly ParallelQuery<Cookie> _query;

        public SuperCookie(IEnumerable<Cookie> cookies)
            : base("fake", "fake")
        {
            _query = cookies.ToConcurrentQuery();
        }

        new public string Domain
        {
            get
            {
                return _query.Select(c => c.Domain).AssertAllMatch().FirstOrDefault();
            }
        }

        new public DateTime? Expiry
        {
            get
            {
                // Seems impractical to expect them all to match here actually
                //return _cookies.Select(c => c.Expiry).AssertAllMatch().FirstOrDefault();
                return _query.FirstOrDefault().Expiry;
            }
        }

        new public string Name
        {
            get
            {
                return _query.Select(c => c.Name).AssertAllMatch().FirstOrDefault();
            }
        }

        new public string Value
        {
            get
            {
                return _query.Select(c => c.Value).AssertAllMatch().FirstOrDefault();
            }
        }

        public override string Path
        {
            get
            {
                return _query.Select(c => c.Path).AssertAllMatch().FirstOrDefault();
            }
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
