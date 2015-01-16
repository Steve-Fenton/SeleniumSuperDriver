using NUnit.Framework;

namespace WebApplicationTests.Steps
{
    [TestFixture]
    public class BaseFixture
    {
        [TestFixtureTearDown]
        public void TearDown()
        {
            DriverSingleton.Driver.Close();
            DriverSingleton.Driver.Quit();
            DriverSingleton.Driver.Dispose();
        }
    }
}
