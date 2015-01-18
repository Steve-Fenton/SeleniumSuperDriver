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
        private readonly ParallelQuery<IWebDriver> _query;

        /// <summary>
        /// <code>
        /// IWebDriver driver = new SuperWebDriver(
        ///     new ChromeDriver(), 
        ///     new FirefoxDriver());
        /// </code>
        /// </summary>
        public SuperWebDriver(params IWebDriver[] drivers)
        {
            _query = drivers.ToConcurrentQuery();
        }

        /// <summary>
        /// <code>
        /// IWebDriver driver = new SuperWebDriver(new IList<IWebDriver> {
        ///     new ChromeDriver(), 
        ///     new FirefoxDriver()
        ///     });
        /// </code>
        /// </summary>
        public SuperWebDriver(IList<IWebDriver> drivers)
        {
            _query = drivers.ToConcurrentQuery();
        }

        public string CurrentWindowHandle
        {
            get
            {
                // Special Note
                // Primitive type. Send back first one.
                return _query.First().CurrentWindowHandle;
            }
        }

        public IOptions Manage()
        {
            return new SuperOptions(_query.Select(d => d.Manage()));
        }

        public INavigation Navigate()
        {
            return new SuperNavigation(_query.Select(d => d.Navigate()));
        }

        public string PageSource
        {
            get
            {
                // Special Note
                // Primitive type. Send back first one.
                return _query.First().PageSource;
            }
        }

        public ITargetLocator SwitchTo()
        {
            return new SuperTargetLocator(_query.Select(d => d.SwitchTo()));
        }

        public string Title
        {
            get
            {
                return _query.Select(d => d.Title).AssertAllMatch().FirstOrDefault();
            }
        }

        public string Url
        {
            get
            {
                return _query.Select(d => d.Url).AssertAllMatch().FirstOrDefault();
            }
            set
            {
                _query.ForAll(d => d.Url = value);
            }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                // Dreaming of!
                //SuperReadOnlyCollection.MergeCollections<string, SuperString>(_drivers.Select(d => d.WindowHandles).ToList());
                return _query.First().WindowHandles;
            }
        }

        public IWebElement FindElement(By by)
        {
            return new SuperWebElement(_query.Select(d => d.FindElement(by)));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return SuperReadOnlyCollection.MergeCollections<IWebElement, SuperWebElement>(_query.Select(d => d.FindElements(by)));
        }

        public void Close()
        {
            _query.ForAll(d => d.Close());
        }

        public void Quit()
        {
            _query.ForAll(d => d.Quit());
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
                _query.ForAll(d => d.Dispose());
            }
        }
    }
}
