using System;
using System.Collections.Generic;

namespace WebApplicationTests
{
    public class TestContext : IDisposable
    {
        private IDisposable _currentPage;
        public IDisposable CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                try
                {
                    if (_currentPage != null)
                    {
                        _currentPage.Dispose();
                    }
                }
                finally
                {
                    _currentPage = value;
                }
            }
        }

        public T GetCurrentPageAs<T>() where T : class, IDisposable
        {
            return CurrentPage as T;
        }

        public void Dispose()
        {
            _currentPage.Dispose();
        }
    }
}
