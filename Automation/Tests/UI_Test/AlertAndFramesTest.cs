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
    internal class AlertAndFramesTest : BaseTest
    {
        private readonly UrlUtilities urlUtilities;
        private readonly HomePage homePage;
        private readonly AlertFrameAndWindowPage alertFrameAndWindowPage;
        private readonly ILog log;

        public AlertAndFramesTest()
        {
            TestLogger.InitializeLog();
            log = LogManager.GetLogger(typeof(AlertAndFramesTest));
            driver = GetDriver(TestContext.Parameters["Browser"]!);
            urlUtilities = new(driver);
            homePage = new(driver);
            alertFrameAndWindowPage = new(driver);
        }

        [SetUp]
        public void AlertAndFramesTestSetUp()
        {
            urlUtilities.NavigateToApplication();
        }

        [TearDown]
        public void AlertAndFramesTestTearDown()
        {
            driver!.CaptureScreenShotForFailedTests();
        }

        [OneTimeTearDown]
        public void AlertAndFramesTestOneTimeTearDown()
        {
            driver!.QuitActiveDriverInstance();
        }

        [Test, Order(1), Category("AlertVerification")]
        public void VerifySimpleAlert()
        {
            log.Info("Verification of simple alert");
            homePage.ClickOnTheHomePageMenu("Alerts, Frame & Windows");
            alertFrameAndWindowPage.ClickOnAlertPageSideMenu("Alerts");
            alertFrameAndWindowPage.ClickOnSimpleAlertButton();
            var alertMessage = driver!.GetAlertMessage();
            Assert.AreEqual("You clicked a button", alertMessage, "Wrong alert message");
            driver!.AcceptAlert();
            log.Info("Verification of simple alert completed");
        }

        [Test, Order(2), Category("AlertVerification")]
        public void VerifyDelayedAlert()
        {
            log.Info("Verification of delayed alert");
            homePage.ClickOnTheHomePageMenu("Alerts, Frame & Windows");
            alertFrameAndWindowPage.ClickOnAlertPageSideMenu("Alerts");
            alertFrameAndWindowPage.ClickOnDelayedAlertButton();
            var delayedalertMessage = driver!.GetAlertMessage();
            Assert.AreEqual("This alert appeared after 5 seconds", delayedalertMessage, "Wrong alert message");
            driver!.AcceptAlert();
            log.Info("Verification of delayed alert completed");
        }

        [Test, Order(3), Category("AlertVerification")]
        public void VerifyAlertWithConfirmButton()
        {
            log.Info("Verification of confirm button in alert");
            homePage.ClickOnTheHomePageMenu("Alerts, Frame & Windows");
            alertFrameAndWindowPage.ClickOnAlertPageSideMenu("Alerts");
            alertFrameAndWindowPage.ClickOnAlertButtonWithConfirmButton();
            var delayedalertMessage = driver!.GetAlertMessage();
            Assert.AreEqual("Do you confirm action?", delayedalertMessage, "Wrong alert message");
            driver!.AcceptAlert();
            var confirmAlertResultText = alertFrameAndWindowPage.GetConfirmAlertResultText();
            Assert.AreEqual("You selected Ok", confirmAlertResultText, "Error in accept alert functionality");
            log.Info("Verification of confirm button in alert confirmed");
        }

        [Test, Order(4), Category("AlertVerification")]
        public void VerifyFrames()
        {
            log.Info("Verify frames");
            homePage.ClickOnTheHomePageMenu("Alerts, Frame & Windows");
            alertFrameAndWindowPage.ClickOnAlertPageSideMenu("Frames");
            string frameElementText = alertFrameAndWindowPage.GetTextFromFrame();
            Assert.AreEqual("This is a sample page", frameElementText, "Wrong text displayed in the frame");
            log.Info("Frames verification completed");
        }

        [Test, Order(5), Category("AlertVerification")]
        public void VerifDialogues()
        {
            log.Info("Verify dialogs");
            homePage.ClickOnTheHomePageMenu("Alerts, Frame & Windows");
            alertFrameAndWindowPage.ClickOnAlertPageSideMenu("Modal Dialogs");
            alertFrameAndWindowPage.ClickOnSmallModelButton();
            var smallDialogBoxText = alertFrameAndWindowPage.GetTextFromDialogBox();
            Assert.AreEqual("This is a small modal. It has very less content", smallDialogBoxText, "Error in  small dialog");
            alertFrameAndWindowPage.CloseDialogBox();
            alertFrameAndWindowPage.ClickOnLargeModelButton();
            var largeDialogBox = alertFrameAndWindowPage.GetTextFromDialogBox();
            Assert.AreNotEqual(smallDialogBoxText, largeDialogBox, "Same message for large and small dialog");
            alertFrameAndWindowPage.CloseDialogBox();
            log.Info("Frames verification completed");
        }
    }
}