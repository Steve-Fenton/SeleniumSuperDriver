using WebApplicationTests.PageObjectModels;

namespace WebApplicationTests
{
    public class TestContext
    {
        private PageBase _currentPage;
        public PageBase CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {

                _currentPage = value;
            }
        }

        public T GetCurrentPageAs<T>() where T : PageBase
        {
            return CurrentPage as T;
        }
    }
}
