using NUnit.Framework;
using TechTalk.SpecFlow;
using WebApplicationTests.PageObjectModels;

namespace WebApplicationTests
{
    [Binding]
    public class TestFormPageSteps
    {
        private TestContext _testContext;

        public TestFormPageSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Given(@"I am on the Test Form page")]
        [Given(@"I navigate to the Test Form page")]
        [When(@"I navigate to the Test Form page")]
        public void GivenIAmOnThe()
        {
            _testContext.CurrentPage = new TestFormPage(DriverSingleton.Driver);
            _testContext.GetCurrentPageAs<TestFormPage>().Open();
            Assert.AreEqual("Sample Application", _testContext.GetCurrentPageAs<TestFormPage>().Title());
            Assert.AreEqual("http://localhost:9822/index.html", _testContext.GetCurrentPageAs<TestFormPage>().Url());
        }

        [Given(@"I have entered the name ""([^\""]*)""")]
        public void GivenIHaveEnteredTheName(string name)
        {
            _testContext.GetCurrentPageAs<TestFormPage>().EnterName(name);
        }

        [Given(@"I have entered the email ""([^\""]*)""")]
        public void GivenIHaveEnteredTheEmail(string email)
        {
            _testContext.GetCurrentPageAs<TestFormPage>().EnterEmail(email);
        }

        [When(@"I press Send")]
        public void WhenIPressSend()
        {
            _testContext.GetCurrentPageAs<TestFormPage>().SubmitForm();
        }

        [Then(@"the result should be ""([^\""]*)""")]
        public void ThenTheResultShouldBe(string expected)
        {
            Assert.AreEqual(expected, _testContext.GetCurrentPageAs<TestFormPage>().Content());
        }
    }
}
