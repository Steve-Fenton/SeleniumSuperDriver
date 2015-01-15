using OpenQA.Selenium;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperCookieJar : ICookieJar
    {
        private readonly ParallelQuery<ICookieJar> _query;

        public SuperCookieJar(IEnumerable<ICookieJar> cookieJars)
        {
            _query = cookieJars.ToConcurrentQuery();
        }

        public ReadOnlyCollection<Cookie> AllCookies
        {
            get
            {
                return SuperReadOnlyCollection.MergeCollections<Cookie, SuperCookie>(_query.Select(j => j.AllCookies));
            }
        }

        public void AddCookie(Cookie cookie)
        {
            _query.ForAll(j => j.AddCookie(cookie));
        }

        public void DeleteAllCookies()
        {
            _query.ForAll(j => j.DeleteAllCookies());
        }

        public void DeleteCookie(Cookie cookie)
        {
            _query.ForAll(j => j.DeleteCookie(cookie));
        }

        public void DeleteCookieNamed(string name)
        {
            _query.ForAll(j => j.DeleteCookieNamed(name));
        }

        public Cookie GetCookieNamed(string name)
        {
            return new SuperCookie(_query.Select(c => c.GetCookieNamed(name)));
        }
    }
}
