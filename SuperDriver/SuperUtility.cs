using OpenQA.Selenium;
using System.Drawing.Imaging;

namespace Fenton.Selenium.SuperDriver
{
    public static class SuperUtility
    {
        /// <summary>
        /// Handles capturing and saving a screenshot if possible - 
        /// whether or not you are using a <c ref="SuperWebDriver">SuperWebDriver</c>.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="fileName"></param>
        /// <param name="imageFormat"></param>
        public static void SaveScreenshot(IWebDriver driver, string fileName, ImageFormat imageFormat)
        {
            var screenshotDriver = driver as ITakesScreenshot;
            if (driver == null)
            {
                return;
            }
            
            var snap = screenshotDriver.GetScreenshot();
            var superSnap = snap as SuperScreenshot;

            if (superSnap == null)
            {
                // Normal screenshot
                snap.SaveAsFile(fileName, imageFormat);
            }
            else
            {
                // Super screenshot
                superSnap.SaveAsFile(fileName, imageFormat);
            }
        }
    }
}
