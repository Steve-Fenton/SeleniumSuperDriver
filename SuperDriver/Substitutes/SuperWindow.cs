using OpenQA.Selenium;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperWindow : IWindow
    {
        private readonly IEnumerable<IWindow> _windows;

        public SuperWindow(IEnumerable<IWindow> windows)
        {
            _windows = windows;
        }

        public void Maximize()
        {
            _windows.AsParallel().ForAll(w => w.Maximize());
        }

        public Point Position
        {
            get
            {
                // Special Note
                // Point is a sealed class. Send back the first one.
                return _windows.First().Position;
            }
            set
            {
                _windows.AsParallel().ForAll(w => w.Position = value);
            }
        }

        public Size Size
        {
            get
            {
                // Special Note
                // Size is a struct. Send back the first one.
                return _windows.First().Size;
            }
            set
            {
                _windows.AsParallel().ForAll(w => w.Size = value);
            }
        }
    }
}
