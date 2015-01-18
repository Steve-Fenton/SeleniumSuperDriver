using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;

namespace WebApplicationTests
{
    [Binding]
    public class CookieSteps
    {
        private TestContext _testContext;
        private Cookie _expectedCookie;
        private Cookie _actualCookie;

        public CookieSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Given(@"I have sent a test cookie to be saved")]
        public void GivenIHaveSentATestCookieToBeSaved()
        {
            var cookieJar = DriverSingleton.Driver.Manage().Cookies;
            cookieJar.DeleteAllCookies();

            _expectedCookie = new Cookie("Test Name", "Test Value");
            cookieJar.AddCookie(_expectedCookie);
        }

        [When(@"I retrieve the test cookie")]
        public void WhenIRetrieveTheTestCookie()
        {
            var cookieJar = DriverSingleton.Driver.Manage().Cookies;
            Assert.AreEqual(1, cookieJar.AllCookies.Count);
            _actualCookie = cookieJar.GetCookieNamed(_expectedCookie.Name);
        }

        [When(@"I delete the test cookie")]
        public void WhenIDeleteTheTestCookie()
        {
            var cookieJar = DriverSingleton.Driver.Manage().Cookies;
            cookieJar.DeleteCookie(_expectedCookie);
        }

        [When(@"I delete the test cookie by name")]
        public void WhenIDeleteTheTestCookieByName()
        {
            var cookieJar = DriverSingleton.Driver.Manage().Cookies;
            cookieJar.DeleteCookieNamed(_expectedCookie.Name);
        }


        [Then(@"the test cookie should be supplied")]
        public void ThenTheTestCookieShouldBeSupplied()
        {
            Assert.AreEqual(_expectedCookie.Secure, _actualCookie.Secure);
            Assert.AreEqual(_expectedCookie.Name, _actualCookie.Name);
            Assert.AreEqual(_expectedCookie.Value, _actualCookie.Value);
        }

        [Then(@"the test cookie should be gone")]
        public void ThenTheTestCookieShouldBeGone()
        {
            var cookieJar = DriverSingleton.Driver.Manage().Cookies;

            Assert.AreEqual(0, cookieJar.AllCookies.Count);
        }
    }
}
