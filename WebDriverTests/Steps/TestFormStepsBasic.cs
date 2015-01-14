using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;

namespace WebApplicationTests
{
    //[Binding]
    //public class TestFormSteps
    //{
    //    private IWebDriver _driver;

    //    private string _testFormUrl = "http://localhost:9822/index.html";

    //    public TestFormSteps()
    //    {
    //        _driver = new FirefoxDriver();
    //        _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
    //    }

    //    [Given(@"I have navigated to the (.*) page")]
    //    public void GivenIHaveNavigatedToTheTestFormPage(string pageName)
    //    {
    //        _driver.Navigate().GoToUrl(_testFormUrl);
    //    }

    //    [Given(@"I have entered the name (.*)")]
    //    public void GivenIHaveEnteredTheNameSteveFenton(string name)
    //    {
    //        _driver.FindElement(By.Id("name")).SendKeys(name);
    //    }

    //    [Given(@"I have entered the email (.*)")]
    //    public void GivenIHaveEnteredTheEmailSteveExample_Com(string email)
    //    {
    //        _driver.FindElement(By.Name("email")).SendKeys(email);
    //    }

    //    [When(@"I press Send")]
    //    public void WhenIPressSend()
    //    {
    //        _driver.FindElement(By.Id("send-button")).Click();
    //    }

    //    [Then(@"the result should be (.*)")]
    //    public void ThenTheResultShouldBeYouEnteredSteveFentonAndSteveExample_Com(string expected)
    //    {
    //        var actual = _driver.FindElement(By.Id("content")).Text;

    //        Assert.AreEqual(expected, actual);
    //    }

    //    [AfterScenario]
    //    public void TearDown()
    //    {
    //        _driver.Quit();
    //    }
    //}
}
