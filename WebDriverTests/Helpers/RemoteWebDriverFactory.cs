using Fenton.Selenium.SuperDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace WebApplicationTests
{
    public class RemoteWebDriverFactory
    {
        public static IWebDriver GetDriver(Browser browser)
        {
            IWebDriver driver = RemoteWebDriverFactory.GetCapabilityFor(browser);
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            return driver;
        }

        public static IWebDriver GetCapabilityFor(Browser browser)
        {
            var uri = new Uri(ConfigurationManager.AppSettings["SeleniumHubUrl"]);
            IWebDriver driver;
            switch (browser)
            {
                case Browser.SuperWebDriver:
                    driver = new SuperWebDriver(GetDriverSuite());
                    break;
                case Browser.Chrome:
                    driver = new RemoteWebDriver(uri, DesiredCapabilities.Chrome());
                    break;
                case Browser.InternetExplorer:
                    driver = new RemoteWebDriver(uri, DesiredCapabilities.InternetExplorer());
                    break;
                default:
                    driver = new RemoteWebDriver(uri, DesiredCapabilities.Firefox());
                    break;
            }
            return driver;
        }

        private static IList<IWebDriver> GetDriverSuite()
        {
            // Allow some degree of parallelism when creating drivers, which can be slow
            IList<IWebDriver> drivers = new List<Func<IWebDriver>>
            {
                () =>  { return GetCapabilityFor(Browser.Chrome); },
                () =>  { return GetCapabilityFor(Browser.Firefox); },
                () => { return GetCapabilityFor(Browser.InternetExplorer); },
            }.AsParallel().Select(d => d()).ToList();

            return drivers;
        }
    }
}
