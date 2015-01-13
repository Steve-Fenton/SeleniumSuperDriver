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
                // Special Note.
                // Undecided on how to proceed with a "SuperString"...
                // For now, we will provide the first window handle.
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
                // Special Note.
                // Undecided on how to proceed with a "SuperString"...
                // For now, we will provide the first page source.
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
                // Special Note.
                // We may substitute a SuperReadOnlyCollection in the future.
                // For now, we will provide the first collection of window handles.
                return _drivers.First().WindowHandles;
            }
        }

        public IWebElement FindElement(By by)
        {
            return new SuperWebElement(_drivers.AsParallel().Select(d => d.FindElement(by)).ToList());
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            IList<ReadOnlyCollection<IWebElement>> webElements = new List<ReadOnlyCollection<IWebElement>>();
            _drivers.AsParallel().ForAll(d => webElements.Add(d.FindElements(by)));

            var countDistribution = webElements.Select(c => c.Count).Distinct();
            var countsMatch = countDistribution.Count() == 1;

            if (!countsMatch)
            {
                throw new MismatchBetweenBrowsersException<int>(countDistribution);
            }

            IList<IWebElement> results = new List<IWebElement>();
            for (var elementsIndex = 0; elementsIndex < countDistribution.SingleOrDefault(); elementsIndex++)
            {
                IList<IWebElement> elementsWithSameIndex = new List<IWebElement>();
                for (var collectionIndex = 0; collectionIndex < webElements.Count; collectionIndex++)
                {
                    elementsWithSameIndex.Add(webElements[collectionIndex][elementsIndex]);
                }
                results.Add(new SuperWebElement(elementsWithSameIndex));
            }

            return new ReadOnlyCollection<IWebElement>(results);
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
