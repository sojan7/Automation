using Application.Pages;
using Application.Utilities;
using Core.Extensions;
using log4net;
using NUnit.Framework;
using Utilities.Utilities;
using Verification.Tests.TestHelpers;

namespace Verification.Tests.UI_Test
{
    [TestFixture]
    internal class NewWindowsTest : BaseTest
    {
        private readonly UrlUtilities urlUtilities;
        private readonly HomePage homePage;
        private readonly AlertFrameAndWindowPage alertFrameAndWindowPage;
        private readonly ILog log;

        public NewWindowsTest()
        {
            TestLogger.InitializeLog();
            log = LogManager.GetLogger(typeof(NewWindowsTest));
            driver = GetDriver(TestContext.Parameters["Browser"]!);
            urlUtilities = new(driver);
            homePage = new(driver);
            alertFrameAndWindowPage = new(driver);
        }

        [SetUp]
        public void NewWindowsTestSetUp()
        {
            urlUtilities.NavigateToApplication();
        }

        [TearDown]
        public void NewWindowsTestTearDown()
        {
            driver!.CaptureScreenShotForFailedTests();
        }

        [OneTimeTearDown]
        public void NewWindowsTestOneTimeTearDown()
        {
            driver!.QuitActiveDriverInstance();
        }

        [Test, Order(1), Category("NewWindowsTest")]
        public void VerifyNewTabNavigation()
        {
            log.Info("Verification of new tab");
            homePage.ClickOnTheHomePageMenu("Alerts, Frame & Windows");
            alertFrameAndWindowPage.ClickOnAlertPageSideMenu("Browser Windows");
            alertFrameAndWindowPage.ClickOnNewTab();
            List<string> windowHandles = new(driver!.WindowHandles);
            Assert.IsTrue(windowHandles.Count == 2, "No new windows are open");
            driver.SwitchTo().Window(windowHandles[1]);
            var newWindowPageSampleHeading = alertFrameAndWindowPage.GetNewWindowPageSampleHeading();
            Assert.AreEqual("This is a sample page", newWindowPageSampleHeading, "Wrong title in new tab");
            driver.Close();
            driver.SwitchTo().Window(windowHandles[0]);
            log.Info("Verification of new tab completed");
        }
    }
}