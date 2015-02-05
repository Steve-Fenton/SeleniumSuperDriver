using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperWebElement : IWebElement
    {
        private readonly ParallelQuery<IWebElement> _query;

        public SuperWebElement(IEnumerable<IWebElement> webElements)
        {
            _query = webElements.ToConcurrentQuery();
        }

        public bool Displayed
        {
            get
            {
                return _query.Select(e => e.Displayed).AssertAllMatch().FirstOrDefault();
            }
        }

        public bool Enabled
        {
            get {
                return _query.Select(e => e.Enabled).AssertAllMatch().FirstOrDefault();
            }
        }

        public Point Location
        {
            get {
                // Special Note
                // Point is a sealed class. Send back the first one.
                return _query.First().Location;
            }
        }

        public bool Selected
        {
            get { return _query.Select(e => e.Selected).AssertAllMatch().FirstOrDefault(); }
        }

        public Size Size
        {
            get {
                // Special Note
                // Size is a struct. Send back the first one.
                return _query.First().Size;
            }
        }

        public string TagName
        {
            get
            {
               return _query.Select(e => e.TagName).AssertAllMatch().FirstOrDefault();
            }
        }

        public string Text
        {
            get
            {
                return _query.Select(e => e.Text).AssertAllMatch().FirstOrDefault();
            }
        }

        public void Clear()
        {
            _query.ForAll(e => e.Clear());
        }

        public void Click()
        {
            _query.ForAll(e => e.Click());
        }

        public string GetAttribute(string attributeName)
        {
            return _query.Select(e => e.GetAttribute(attributeName)).AssertAllMatch().FirstOrDefault();
        }

        public string GetCssValue(string propertyName)
        {
            return _query.Select(e => e.GetCssValue(propertyName)).AssertAllMatch().FirstOrDefault();
        }

        public void SendKeys(string text)
        {
            _query.ForAll(e => e.SendKeys(text));
        }

        public void Submit()
        {
            _query.ForAll(e => e.Submit());
        }

        public IWebElement FindElement(By by)
        {
            return new SuperWebElement(_query.Select(e => e.FindElement(by)));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return SuperReadOnlyCollection.MergeCollections<IWebElement, SuperWebElement>(_query.Select(e => e.FindElements(by)));
        }
    }
}
