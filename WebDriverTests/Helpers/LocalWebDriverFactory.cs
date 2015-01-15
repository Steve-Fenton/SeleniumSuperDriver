using Fenton.Selenium.SuperDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplicationTests
{
    public static class LocalWebDriverFactory
    {
        internal static IWebDriver GetDriver(Browser browser)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case Browser.SuperWebDriver:
                    driver = new SuperWebDriver(GetDriverSuite());
                    break;
                case Browser.Chrome:
                    driver = new ChromeDriver();
                    break;
                case Browser.InternetExplorer:
                    driver = new InternetExplorerDriver(new InternetExplorerOptions() { IntroduceInstabilityByIgnoringProtectedModeSettings = true });
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }

            // Implicit wait of up to 10 seconds
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            return driver;
        }

        private static IList<IWebDriver> GetDriverSuite()
        {
            // Allow some degree of parallelism when creating drivers, which can be slow
            IList<IWebDriver> drivers = new List<Func<IWebDriver>>
            {
                () =>  { return GetDriver(Browser.Chrome); },
                () =>  { return GetDriver(Browser.Firefox); },
                () => { return GetDriver(Browser.InternetExplorer); },
                () =>  { return GetDriver(Browser.Chrome); },
                () =>  { return GetDriver(Browser.Firefox); },
                () => { return GetDriver(Browser.InternetExplorer); },
            }.AsParallel().Select(d => d()).ToList();

            return drivers;
        }
    }
}
