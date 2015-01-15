using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace WebApplicationTests.PageObjectModels
{
    public class PageBase
    {
        protected IWebDriver Driver;

        public PageBase(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(Driver, this);
        }
    }
}
