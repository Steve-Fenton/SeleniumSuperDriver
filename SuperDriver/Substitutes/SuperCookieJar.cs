using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperCookieJar : ICookieJar
    {
        private readonly IEnumerable<ICookieJar> _cookieJars;

        public SuperCookieJar(IEnumerable<ICookieJar> cookieJars)
        {
            _cookieJars = cookieJars;
        }

        public ReadOnlyCollection<Cookie> AllCookies
        {
            get
            {
                return SuperReadOnlyCollection.MergeCollections<Cookie, SuperCookie>(_cookieJars.AsParallel().Select(j => j.AllCookies).ToList());
            }
        }

        public void AddCookie(Cookie cookie)
        {
            _cookieJars.AsParallel().ForAll(j => j.AddCookie(cookie));
        }

        public void DeleteAllCookies()
        {
            _cookieJars.AsParallel().ForAll(j => j.DeleteAllCookies());
        }

        public void DeleteCookie(Cookie cookie)
        {
            _cookieJars.AsParallel().ForAll(j => j.DeleteCookie(cookie));
        }

        public void DeleteCookieNamed(string name)
        {
            _cookieJars.AsParallel().ForAll(j => j.DeleteCookieNamed(name));
        }

        public Cookie GetCookieNamed(string name)
        {
            return new SuperCookie(_cookieJars.AsParallel().Select(c => c.GetCookieNamed(name)).ToList());
        }
    }
}
