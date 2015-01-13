using System;
using System.Collections.Generic;

namespace Fenton.Selenium.SuperDriver
{
    [Serializable]
    public class SuperException : ApplicationException
    {
        public SuperException(string message) : base(message) { }
    }
}
