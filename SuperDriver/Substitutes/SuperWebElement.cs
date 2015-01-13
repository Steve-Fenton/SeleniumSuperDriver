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
                // Special Note
                // Point is a sealed class. Send back the first one.
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
                // Special Note
                // Size is a struct. Send back the first one.
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
            return new SuperReadOnlyCollection<IWebElement>(_webElements.AsParallel().Select(e => e.FindElements(by)).ToList());
        }
    }
}
