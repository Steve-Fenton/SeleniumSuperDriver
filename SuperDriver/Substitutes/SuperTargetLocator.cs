using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperTargetLocator : ITargetLocator
    {
        private readonly IEnumerable<ITargetLocator> _targetLocators;

        public SuperTargetLocator(IEnumerable<ITargetLocator> targetLocators)
        {
            _targetLocators = targetLocators;
        }

        public IWebElement ActiveElement()
        {
            return new SuperWebElement(_targetLocators.AsParallel().Select(l => l.ActiveElement()).ToList());
        }

        public IAlert Alert()
        {
            throw new NotImplementedException();
        }

        public IWebDriver DefaultContent()
        {
            return new SuperWebDriver(_targetLocators.AsParallel().Select(l => l.DefaultContent()).ToList());
        }

        public IWebDriver Frame(IWebElement frameElement)
        {
            return new SuperWebDriver(_targetLocators.AsParallel().Select(l => l.Frame(frameElement)).ToList());
        }

        public IWebDriver Frame(string frameName)
        {
            return new SuperWebDriver(_targetLocators.AsParallel().Select(l => l.Frame(frameName)).ToList());
        }

        public IWebDriver Frame(int frameIndex)
        {
            return new SuperWebDriver(_targetLocators.AsParallel().Select(l => l.Frame(frameIndex)).ToList());
        }

        public IWebDriver ParentFrame()
        {
            return new SuperWebDriver(_targetLocators.AsParallel().Select(l => l.ParentFrame()).ToList());
        }

        public IWebDriver Window(string windowName)
        {
            return new SuperWebDriver(_targetLocators.AsParallel().Select(l => l.Window(windowName)).ToList());
        }
    }
}
