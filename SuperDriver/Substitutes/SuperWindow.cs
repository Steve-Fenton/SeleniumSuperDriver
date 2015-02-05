using OpenQA.Selenium;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperWindow : IWindow
    {
        private readonly ParallelQuery<IWindow> _query;

        public SuperWindow(IEnumerable<IWindow> windows)
        {
            _query = windows.ToConcurrentQuery();
        }

        public void Maximize()
        {
            _query.ForAll(w => w.Maximize());
        }

        public Point Position
        {
            get
            {
                // Special Note
                // Point is a sealed class. Send back the first one.
                return _query.First().Position;
            }
            set
            {
                _query.ForAll(w => w.Position = value);
            }
        }

        public Size Size
        {
            get
            {
                // Special Note
                // Size is a struct. Send back the first one.
                return _query.First().Size;
            }
            set
            {
                _query.ForAll(w => w.Size = value);
            }
        }
    }
}
