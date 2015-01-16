using OpenQA.Selenium;
using System;
using System.Configuration;
using WebApplicationTests.PageObjectModels;

namespace WebApplicationTests
{
    public static class DriverSingleton
    {
        private static IWebDriver _driver;
        private static Object _driverLock = new Object();
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    lock (_driverLock)
                    {
                        if (_driver == null)
                        {
                            var isLocal = bool.Parse(ConfigurationManager.AppSettings["IsLocal"]);
                            var browser = ConfigurationManager.AppSettings["Browser"].AsEnum<Browser>();

                            _driver = (isLocal) ?
                                LocalWebDriverFactory.GetWebDriver(browser) :
                                RemoteWebDriverFactory.GetWebDriver(browser);
                        }
                    }
                }
                return _driver;
            }
        }
    }
}
