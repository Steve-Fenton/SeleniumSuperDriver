using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperScreenshot : Screenshot
    {
        private readonly IList<Screenshot> _screenshots;

        public SuperScreenshot(IEnumerable<Screenshot> screenshots)
            : base(screenshots.First().AsBase64EncodedString)
        {
            _screenshots = screenshots.ToList();
        }

        new public void SaveAsFile(string fileName, ImageFormat format)
        {
            foreach (var screenshot in _screenshots) { 
                var name = string.Format("{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(fileName),
                    Guid.NewGuid(),
                    Path.GetExtension(fileName));

                screenshot.SaveAsFile(Path.Combine(Path.GetDirectoryName(fileName), name), format);
            };
        }
    }
}
