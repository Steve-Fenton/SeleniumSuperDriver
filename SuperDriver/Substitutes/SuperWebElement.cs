using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperWebElement : IWebElement
    {
        private readonly IEnumerable<IWebElement> _webElements;

        public SuperWebElement(IEnumerable<IWebElement> webElements)
        {
            _webElements = webElements;
        }

        public bool Displayed
        {
            get
            {
                return _webElements.AsParallel().Select(e => e.Displayed).AssertAllMatch().FirstOrDefault();
            }
        }

        public bool Enabled
        {
            get {
                return _webElements.AsParallel().Select(e => e.Enabled).AssertAllMatch().FirstOrDefault();
            }
        }

        public Point Location
        {
            get {
                // Special Note.
                // We may substitute a SuperPoint in the future.
                // For now, we will provide the first point.
                return _webElements.First().Location;
            }
        }

        public bool Selected
        {
            get { return _webElements.AsParallel().Select(e => e.Selected).AssertAllMatch().FirstOrDefault(); }
        }

        public Size Size
        {
            get {
                // Special Note.
                // We may substitute a SuperSize in the future.
                // For now, we will provide the first size.
                return _webElements.First().Size;
            }
        }

        public string TagName
        {
            get
            {
               return _webElements.AsParallel().Select(e => e.TagName).AssertAllMatch().FirstOrDefault();
            }
        }

        public string Text
        {
            get
            {
                return _webElements.AsParallel().Select(e => e.Text).AssertAllMatch().FirstOrDefault();
            }
        }

        public void Clear()
        {
            _webElements.AsParallel().ForAll(e => e.Clear());
        }

        public void Click()
        {
            _webElements.AsParallel().ForAll(e => e.Click());
        }

        public string GetAttribute(string attributeName)
        {
            return _webElements.AsParallel().Select(e => e.GetAttribute(attributeName)).AssertAllMatch().FirstOrDefault();
        }

        public string GetCssValue(string propertyName)
        {
            return _webElements.AsParallel().Select(e => e.GetCssValue(propertyName)).AssertAllMatch().FirstOrDefault();
        }

        public void SendKeys(string text)
        {
            _webElements.AsParallel().ForAll(e => e.SendKeys(text));
        }

        public void Submit()
        {
            _webElements.AsParallel().ForAll(e => e.Submit());
        }

        public IWebElement FindElement(By by)
        {
            return new SuperWebElement(_webElements.AsParallel().Select(e => e.FindElement(by)).ToList());
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            IList<ReadOnlyCollection<IWebElement>> webElements = new List<ReadOnlyCollection<IWebElement>>();
            _webElements.AsParallel().ForAll(d => webElements.Add(d.FindElements(by)));

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
    }
}
