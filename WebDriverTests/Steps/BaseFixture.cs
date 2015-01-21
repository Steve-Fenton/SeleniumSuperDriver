using Fenton.Selenium.SuperDriver;
using System;
using System.Drawing.Imaging;
using System.IO;
using TechTalk.SpecFlow;

namespace WebApplicationTests.Steps
{
    [Binding]
    public class BaseFixture
    {
        [AfterScenario]
        public static void AfterScenario()
        {
            if (ScenarioContext.Current.TestError == null)
            {
                return;
            }

            SuperUtility.SaveScreenshot(DriverSingleton.Driver, GetFileName(), ImageFormat.Jpeg);
        }

        private static string GetFileName()
        {
            var location = @"C:/Temp/Screenshots";
            Directory.CreateDirectory(location);
            var fileName = string.Format("Failure_{0}.jpg", DateTime.UtcNow.ToFileTimeUtc());
            return Path.Combine(location, fileName);
        }

        [AfterTestRun]
        public static void TearDown()
        {
            DriverSingleton.Driver.Close();
            DriverSingleton.Driver.Quit();
            DriverSingleton.Driver.Dispose();
        }
    }
}
