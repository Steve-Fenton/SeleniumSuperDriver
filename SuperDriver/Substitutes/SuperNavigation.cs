using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperNavigation : INavigation
    {
        private readonly ParallelQuery<INavigation> _query;

        public SuperNavigation(IEnumerable<INavigation> navigations)
        {
            _query = navigations.ToConcurrentQuery();
        }

        public void Back()
        {
            _query.ForAll(n => n.Back());
        }

        public void Forward()
        {
            _query.ForAll(n => n.Forward());
        }

        public void GoToUrl(Uri url)
        {
            _query.ForAll(n => n.GoToUrl(url));
        }

        public void GoToUrl(string url)
        {
            _query.ForAll(n => n.GoToUrl(url));
        }

        public void Refresh()
        {
            _query.ForAll(n => n.Refresh());
        }
    }
}
