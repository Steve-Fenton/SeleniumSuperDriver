using OpenQA.Selenium;
using System;
using System.Configuration;

namespace WebApplicationTests.PageObjectModels
{
    public class TestNavigationPage : PageBase
    {
        private Uri _testFormUri;

        public TestNavigationPage(IWebDriver driver)
            : base(driver)
        {
            _testFormUri = new Uri(new Uri(ConfigurationManager.AppSettings["TestUrl"]), "index.html#Navigation");
        }

        public void Open()
        {
            Driver.Navigate().GoToUrl(_testFormUri);
        }
    }
}
