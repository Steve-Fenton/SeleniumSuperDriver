using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using WebApplicationTests.PageObjectModels;

namespace WebApplicationTests
{
    [Binding]
    public class AlertSteps
    {
        private TestContext _testContext;
        private string _windowHandle;

        public AlertSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Given(@"I have triggered an alert")]
        public void GivenIHaveTriggeredAnAlert()
        {
            _windowHandle = DriverSingleton.Driver.CurrentWindowHandle;
            _testContext.GetCurrentPageAs<TestFormPage>().TriggerAlert();
        }

        [When(@"I accept the alert")]
        public void WhenIAcceptTheAlert()
        {
            var alert = DriverSingleton.Driver.SwitchTo().Alert();
            Assert.AreEqual("Test alert message.", alert.Text);
            alert.Accept();

            DriverSingleton.Driver.SwitchTo().Window(_windowHandle);
        }

        [When(@"I dismiss the alert")]
        public void WhenIDismissTheAlert()
        {
            DriverSingleton.Driver.SwitchTo().Alert().Dismiss();

            DriverSingleton.Driver.SwitchTo().Window(_windowHandle);
        }

        [Then(@"the alert should close")]
        public void ThenTheAlertShouldClose()
        {
            var exceptionHandled = false;
            try
            {
                DriverSingleton.Driver.SwitchTo().Alert();
            }
            catch (AggregateException ex)
            {
                Assert.AreEqual(typeof(NoAlertPresentException), ex.InnerException.GetType());
                exceptionHandled = true;
            }
            finally
            {
                DriverSingleton.Driver.SwitchTo().Window(_windowHandle);

                Assert.IsTrue(exceptionHandled);
            }
        }

    }
}
