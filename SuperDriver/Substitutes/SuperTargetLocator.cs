using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
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
            throw new NotImplementedException();
        }

        public IWebDriver DefaultContent()
        {
            return new SuperWebDriver(_query.Select(l => l.DefaultContent()).ToList());
        }

        public IWebDriver Frame(IWebElement frameElement)
        {
            return new SuperWebDriver(_query.Select(l => l.Frame(frameElement)).ToList());
        }

        public IWebDriver Frame(string frameName)
        {
            return new SuperWebDriver(_query.Select(l => l.Frame(frameName)).ToList());
        }

        public IWebDriver Frame(int frameIndex)
        {
            return new SuperWebDriver(_query.Select(l => l.Frame(frameIndex)).ToList());
        }

        public IWebDriver ParentFrame()
        {
            return new SuperWebDriver(_query.Select(l => l.ParentFrame()).ToList());
        }

        public IWebDriver Window(string windowName)
        {
            return new SuperWebDriver(_query.Select(l => l.Window(windowName)).ToList());
        }
    }
}
