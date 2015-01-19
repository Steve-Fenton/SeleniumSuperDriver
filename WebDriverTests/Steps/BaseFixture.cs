using NUnit.Framework;
using TechTalk.SpecFlow;

namespace WebApplicationTests.Steps
{
    [Binding]
    public class BaseFixture
    {
        [AfterTestRun]
        public static void TearDown()
        {
            DriverSingleton.Driver.Close();
            DriverSingleton.Driver.Quit();
            DriverSingleton.Driver.Dispose();
        }
    }
}
