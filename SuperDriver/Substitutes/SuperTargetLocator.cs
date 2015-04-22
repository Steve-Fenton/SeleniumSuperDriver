using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperTargetLocator : ITargetLocator
    {
        private readonly ParallelQuery<ITargetLocator> _query;

        public SuperTargetLocator(IEnumerable<ITargetLocator> targetLocators)
        {
            _query = targetLocators.ToConcurrentQuery();
        }

        public IWebElement ActiveElement()
        {
            return new SuperWebElement(_query.Select(l => l.ActiveElement()));
        }

        public IAlert Alert()
        {
            return new SuperAlert(_query.Select(l => l.Alert()));
        }

        public IWebDriver DefaultContent()
        {
            return new SuperWebDriver(_query.Select(l => l.DefaultContent()));
        }

        public IWebDriver Frame(IWebElement frameElement)
        {
            return new SuperWebDriver(_query.Select(l => l.Frame(frameElement)));
        }

        public IWebDriver Frame(string frameName)
        {
            return new SuperWebDriver(_query.Select(l => l.Frame(frameName)));
        }

        public IWebDriver Frame(int frameIndex)
        {
            return new SuperWebDriver(_query.Select(l => l.Frame(frameIndex)));
        }

        public IWebDriver ParentFrame()
        {
            return new SuperWebDriver(_query.Select(l => l.ParentFrame()));
        }

        public IWebDriver Window(string windowName)
        {
            var windowNames = windowName.Split('|');
            var webDrivers = new List<IWebDriver>();

            foreach (var name in windowNames)
            {
                foreach (var tl in _query)
                {
                    try
                    {
                        webDrivers.Add(tl.Window(name));
                    }
                    catch (NoSuchWindowException) { };
                }
            }

            return new SuperWebDriver(webDrivers);
        }
    }
}
