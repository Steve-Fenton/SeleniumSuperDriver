using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Configuration;

namespace WebApplicationTests.PageObjectModels
{
    public class TestFormPage : PageBase
    {
        private Uri _testFormUri;

        [FindsBy(How = How.Id, Using = "name")]
        public IWebElement NameElement { get; set; }

        [FindsBy(How = How.Name, Using = "email")]
        public IWebElement EmailElement { get; set; }

        [FindsBy(How = How.Id, Using = "send-button")]
        public IWebElement SendButton { get; set; }

        [FindsBy(How = How.Id, Using = "content")]
        public IWebElement ContentElement { get; set; }

        [FindsBy(How = How.Id, Using = "alerter")]
        public IWebElement AlertLinkElement { get; set; }

        public TestFormPage(IWebDriver driver) : base(driver)
        {
            _testFormUri = new Uri(new Uri(ConfigurationManager.AppSettings["TestUrl"]), "index.html");
        }

        public void Open()
        {
            Driver.Navigate().GoToUrl(_testFormUri.AbsoluteUri);
        }

        public void EnterName(string name)
        {
            NameElement.Clear();
            NameElement.SendKeys(name);
        }

        public void EnterEmail(string email)
        {
            EmailElement.Clear();
            EmailElement.SendKeys(email);
        }

        public void SubmitForm()
        {
            SendButton.Click();
        }

        public string Content()
        {
            return ContentElement.Text;
        }

        internal void TriggerAlert()
        {
            AlertLinkElement.Click();
        }
    }
}
