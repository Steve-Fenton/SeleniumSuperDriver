using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;
using WebApplicationTests.PageObjectModels;

namespace WebApplicationTests
{
    [Binding]
    public class WindowSteps
    {
        private TestContext _testContext;

        public WindowSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [When(@"I maximise the browser")]
        public void WhenIMaximiseTheBrowser()
        {
            DriverSingleton.Driver.Manage().Window.Maximize();
        }

        [Then(@"all browsers should be maximised")]
        public void ThenAllBrowsersShouldBeMaximised()
        {
            var window = DriverSingleton.Driver.Manage().Window;

            /* 
             * Although there is no assertion, each browser
             * must supply the same answers for this test to pass -
             * this is checked via the SuperExtensions methods.
             */
            var pos = window.Position;
            var size = window.Size;
        }
    }
}
