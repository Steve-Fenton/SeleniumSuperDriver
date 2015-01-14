using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;

namespace WebApplicationTests.PageObjectModels
{
    public class TestFormPage : IDisposable
    {
        private IWebDriver _driver;
        private string _testFormUrl;

        public TestFormPage(Browser browser)
        {
            // Local run
            _driver = LocalWebDriverFactory.GetDriver(browser);

            // Remote run - see SeleniumServer.txt
            //_driver = RemoteWebDriverFactory.GetDriver(browser);

            Uri pageUri = new Uri(new Uri(ConfigurationManager.AppSettings["TestUrl"]), "index.html");
            _testFormUrl = pageUri.AbsoluteUri;
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl(_testFormUrl);
        }

        public void EnterName(string name)
        {
            var nameElement = _driver.FindElement(By.Id("name"));
            nameElement.SendKeys(name);
        }

        public void EnterEmail(string email)
        {
            var emailElement = _driver.FindElement(By.Name("email"));
            emailElement.SendKeys(email);
        }

        public void SubmitForm()
        {
            var sendElement = _driver.FindElement(By.Id("send-button"));
            sendElement.Click();

            var elems = _driver.FindElements(By.CssSelector("input:invalid"));
            Console.WriteLine("I found: " + elems.Count);
        }

        public bool ContentMatches(string expected)
        {
            var contentElement = _driver.FindElement(By.Id("content"));
            return contentElement.Text.Equals(expected);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
