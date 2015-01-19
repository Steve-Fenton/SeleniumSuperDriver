using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;
using WebApplicationTests.PageObjectModels;

namespace WebApplicationTests
{
    [Binding]
    public class NavigationSteps
    {
        private TestContext _testContext;

        public NavigationSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Given(@"I am on the Test Navigation page")]
        [Given(@"I navigate to the Test Navigation page")]
        [When(@"I navigate to the Test Navigation page")]
        public void WhenINavigateToTheTestNavigationPage()
        {
            _testContext.CurrentPage = new TestNavigationPage(DriverSingleton.Driver);
            _testContext.GetCurrentPageAs<TestNavigationPage>().Open();
        }

        [Given(@"I navigate back")]
        [When(@"I navigate back")]
        public void WhenINavigateBack()
        {
            DriverSingleton.Driver.Navigate().Back();
        }

        [When(@"I navigate forward")]
        public void WhenINavigateForward()
        {
            DriverSingleton.Driver.Navigate().Forward();
        }


        [Then(@"I should be on the Test Navigation page")]
        public void ThenIShouldBeOnTheTestNavigationPage()
        {
            var actual = DriverSingleton.Driver.Url;
            Assert.IsTrue(actual.Contains("#Navigation"));
        }

    }
}
