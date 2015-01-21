using Fenton.Selenium.SuperDriver;
using NUnit.Framework;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace WebApplicationTests
{
    [Binding]
    public class ScreenshotSteps
    {
        private TestContext _testContext;
        private const string _tempPath = @"C:\Temp\SuperWebDriver\Screenshots\Volatile\";
        private DirectoryInfo _tempDirectory;

        public ScreenshotSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Given(@"I have an empty screenshot folder")]
        public void GivenIHaveAnEmptyScreenshotFolder()
        {
            _tempDirectory = new DirectoryInfo(_tempPath);
            _tempDirectory.Create();

            _tempDirectory.Delete(true);
            _tempDirectory.Create();
        }


        [When(@"I take a screenshot")]
        public void WhenITakeAScreenshot()
        {
            var fileName = Path.Combine(_tempPath, "temp.jpg");
            SuperUtility.SaveScreenshot(DriverSingleton.Driver, fileName, ImageFormat.Jpeg);
        }

        [Then(@"the screenshot should be saved")]
        public void ThenTheScreenshotShouldBeSaved()
        {
            var browserCount = (DriverSingleton.Driver as SuperWebDriver).GetBrowserCount();
            Assert.AreEqual(browserCount, _tempDirectory.GetFiles().Count());
        }

    }
}
