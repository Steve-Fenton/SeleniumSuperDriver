using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    /// <summary>
    /// An <see cref="OpenQA.Selenium.IWebDriver"/> that distributes commands to multiple drivers in parallel.
    /// </summary>
    public class SuperWebDriver : IWebDriver
    {
        private readonly IEnumerable<IWebDriver> _drivers;

        /// <summary>
        /// <code>
        /// IWebDriver driver = new SuperWebDriver(
        ///     new ChromeDriver(), 
        ///     new FirefoxDriver());
        /// </code>
        /// </summary>
        public SuperWebDriver(params IWebDriver[] drivers)
        {
            _drivers = drivers;
        }

        public SuperWebDriver(IList<IWebDriver> drivers)
        {
            _drivers = drivers;
        }

        public void Close()
        {
            _drivers.AsParallel().ForAll(d => d.Close());
        }

        public string CurrentWindowHandle
        {
            get
            {
                // Special Note
                // Primitive type. Send back first one.
                return _drivers.First().CurrentWindowHandle;
            }
        }

        public IOptions Manage()
        {
            return new SuperOptions(_drivers.AsParallel().Select(d => d.Manage()).ToList());
        }

        public INavigation Navigate()
        {
            return new SuperNavigation(_drivers.AsParallel().Select(d => d.Navigate()).ToList());
        }

        public string PageSource
        {
            get
            {
                // Special Note
                // Primitive type. Send back first one.
                return _drivers.First().PageSource;
            }
        }

        public void Quit()
        {
            _drivers.AsParallel().ForAll(d => d.Quit());
        }

        public ITargetLocator SwitchTo()
        {
            return new SuperTargetLocator(_drivers.AsParallel().Select(d => d.SwitchTo()).ToList());
        }

        public string Title
        {
            get
            {
                return _drivers.AsParallel().Select(d => d.Title).AssertAllMatch().FirstOrDefault();
            }
        }

        public string Url
        {
            get
            {
                return _drivers.AsParallel().Select(d => d.Url).AssertAllMatch().FirstOrDefault();
            }
            set
            {
                _drivers.AsParallel().ForAll(d => d.Url = value);
            }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                return new SuperReadOnlyCollection<string>(_drivers.AsParallel().Select(d => d.WindowHandles).ToList());
            }
        }

        public IWebElement FindElement(By by)
        {
            return new SuperWebElement(_drivers.AsParallel().Select(d => d.FindElement(by)).ToList());
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return new SuperReadOnlyCollection<IWebElement>(_drivers.AsParallel().Select(d => d.FindElements(by)).ToList());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _drivers.AsParallel().ForAll(d => d.Dispose());
            }
        }
    }
}
