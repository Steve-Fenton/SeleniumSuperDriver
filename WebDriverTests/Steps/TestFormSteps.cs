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

        [Given(@"I am using the ""([^\""]*)"" browser")]
        public void GivenIAmUsingTheBrowser(string browser)
        {
        }

        [Given(@"I am on the Test Form page")]
        public void GivenIAmOnThe()
        {
            _testContext.CurrentPage = new TestFormPage(_testContext.Driver);
            _testContext.GetCurrentPageAs<TestFormPage>().Open();
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

        [AfterScenario]
        public void TearDown()
        {
            _testContext.Dispose();
        }
    }
}
