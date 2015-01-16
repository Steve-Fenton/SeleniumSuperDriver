using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;

namespace WebApplicationTests
{
    [Binding]
    public class FindElementsSteps
    {
        private TestContext _testContext;
        private ReadOnlyCollection<IWebElement> _results;
        private IWebElement _result;

        public FindElementsSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [When(@"I select elements ""([^\""]*)"" ""([^\""]*)""")]
        public void WhenISelectElements(string by, string selector)
        {
            _results = DriverSingleton.Driver.FindElements(GetSelector(by, selector));
        }

        [When(@"I select an element ""([^\""]*)"" ""([^\""]*)""")]
        public void WhenISelectAnElement(string by, string selector)
        {
            _result = DriverSingleton.Driver.FindElement(GetSelector(by, selector));
        }

        [Then(@"there should be (.*)")]
        public void ThenThereShouldBe(int numberOfMatches)
        {
            Assert.AreEqual(numberOfMatches, _results.Count);
        }

        [Then(@"the text should be ""([^\""]*)""")]
        public void ThenTheTextShouldBe(string content)
        {
            Assert.AreEqual(content, _result.Text);
        }

        private By GetSelector(string by, string selector)
        {
            switch (by)
            {
                case "TagName":
                    return By.TagName(selector);
                case "ClassName":
                    return By.ClassName(selector);
                case "CssSelector":
                    return By.CssSelector(selector);
                case "Id":
                    return By.Id(selector);
                case "LinkText":
                    return By.LinkText(selector);
                case "Name":
                    return By.Name(selector);
                case "PartialLinkText":
                    return By.PartialLinkText(selector);
                case "XPath":
                    return By.XPath(selector);
            }

            throw new ArgumentException("By not known: " + by);
        }
    }
}
