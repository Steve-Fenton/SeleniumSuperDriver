using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperNavigation : INavigation
    {
        private readonly IEnumerable<INavigation> _navigations;

        public SuperNavigation(IEnumerable<INavigation> navigations)
        {
            _navigations = navigations;
        }

        public void Back()
        {
            _navigations.AsParallel().ForAll(n => n.Back());
        }

        public void Forward()
        {
            _navigations.AsParallel().ForAll(n => n.Forward());
        }

        public void GoToUrl(Uri url)
        {
            _navigations.AsParallel().ForAll(n => n.GoToUrl(url));
        }

        public void GoToUrl(string url)
        {
            _navigations.AsParallel().ForAll(n => n.GoToUrl(url));
        }

        public void Refresh()
        {
            _navigations.AsParallel().ForAll(n => n.Refresh());
        }
    }
}
