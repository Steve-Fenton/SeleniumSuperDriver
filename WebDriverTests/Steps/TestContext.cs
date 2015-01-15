using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using WebApplicationTests.PageObjectModels;

namespace WebApplicationTests
{
    public class TestContext : IDisposable
    {
        private PageBase _currentPage;
        public PageBase CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {

                _currentPage = value;
            }
        }

        private IWebDriver _driver;
        public IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    var isLocal = bool.Parse(ConfigurationManager.AppSettings["IsLocal"]);
                    var browser = ConfigurationManager.AppSettings["Browser"].AsEnum<Browser>();

                    _driver = (isLocal) ?
                        LocalWebDriverFactory.GetDriver(browser) :
                        RemoteWebDriverFactory.GetDriver(browser);
                }
                return _driver;
            }
        }

        public T GetCurrentPageAs<T>() where T : PageBase
        {
            return CurrentPage as T;
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}
