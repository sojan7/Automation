using NUnit.Framework;
using Core.Extensions;
using Application.Utilities;
using Application.Pages;
using log4net;
using Utilities.Utilities;
using Utilities.Models;
using Verification.Tests.TestHelpers;

namespace Verification.Tests.UI_Test
{
    /// <summary>
    /// A parallelized sample test fixture
    /// Current implementation will not help in parallel test run in 1 fixture.
    /// Driver generation is done as part of constructor
    /// </summary>
    [TestFixture]
    [Parallelizable]
    internal class ElementsPageTest : BaseTest
    {
        private readonly UrlUtilities urlUtilities;
        private readonly HomePage homePage;
        private readonly ElementsPage elementsPage;
        private readonly ILog log;

        public ElementsPageTest()
        {
            TestLogger.InitializeLog();
            log = LogManager.GetLogger(typeof(ElementsPageTest));
            driver = GetDriver(TestContext.Parameters["Browser"]!);
            urlUtilities = new(driver);
            homePage = new(driver);
            elementsPage = new(driver);
        }

        [SetUp]
        public void ElementsPageTestSetUp()
        {
            urlUtilities.NavigateToApplication();
        }

        [TearDown]
        public void ElementsPageTestTearDown()
        {
            driver!.CaptureScreenShotForFailedTests();
        }

        [OneTimeTearDown]
        public void ElementsPageTestOneTimeTearDown()
        {
            driver!.QuitActiveDriverInstance();
        }

        [Test, Order(1), Category("ElementsPageVerification")]
        public void VerifyAllExpectedMenusArePresentInHomePage()
        {
            log.Info("Verification that all expected menus are displayed in homepage");
            List<string> expectedMenus = new()
            {
            "Elements",
            "Forms",
            "Alerts, Frame & Windows",
            "Widgets",
            "Interactions",
            "Book Store Application"
            };
            List<bool> allHomePageMenuVisibility = homePage.IsHomePageMenusVisible(expectedMenus);
            Assert.IsTrue(allHomePageMenuVisibility.All(item => item), "All the expected menus are not present in the UI");
            log.Info("Verified that all expected menus are displayed in homepage");
        }

        [Test, Order(2), Category("ElementsPageVerification")]
        public void VerifyExceptionCaseForElementsPage()
        {
            log.Info("Verification that Text Bx is not displayed");
            homePage.ClickOnTheHomePageMenu("Elements");
            Assert.IsFalse(elementsPage.IsASpecificMenuVisbleInUi("Text Bx"));
            log.Info("Verified that Text Bx is not displayed");
        }

        [Test, Order(3), Category("ElementsPageVerification")]
        public void VerifyErrorForInvalidEmailInTextBoxElementsPage()
        {
            log.Info("Verification of invalid email in the elements page");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Text Box");
            Assert.IsTrue(elementsPage.GetVisibilityOfInvalidEmailError("sojan"), "Error is not shown when invalid email id is entered");
            log.Info("Verified invalid email in the elements page completed");
        }

        [Test, Order(4), Category("ElementsPageVerification")]
        public void VerifyUserDetailsFillInElementsPage()
        {
            log.Info("Verification of user details fill in the elements page");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Text Box");
            elementsPage.FillUserFullName("Sojan Somarajan");
            elementsPage.FillUserEmailId("sojan@epam.com");
            elementsPage.EnterCurrentAddress("Current address\nTesting new line");
            elementsPage.EnterPermanentAddress("Permanent address\nTesting new line");
            elementsPage.ClickOnTextBoxSubmitButton();
            var actualTextBoxOutPutText = elementsPage.GetTextBoxOutPutText();
            var expectedTextBoxOutPutText = "Name:Sojan Somarajan\r\nEmail:sojan@epam.com\r\nCurrent Address :Current address Testing new line\r\nPermananet Address :Permanent address Testing new line";
            Assert.AreEqual(expectedTextBoxOutPutText, actualTextBoxOutPutText.ToString());
            log.Info("Verified user details fill in the elements page");
        }

        [Test, Order(5), Category("ElementsPageVerification")]
        public void VerifyCheckBoxInElementsPage()
        {
            log.Info("Verification of check box in the elements page");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Check Box");
            elementsPage.SelectWordFileCheckBox();
            var actualCheckBoxOutput = elementsPage.GetCheckBoxResult();
            var expectedCheckBoxOutput = "You have selected :\r\nwordFile";
            Assert.AreEqual(expectedCheckBoxOutput, actualCheckBoxOutput);
            log.Info("Verified check box in the elements page");
        }

        [Test, Order(6), Category("ElementsPageVerification")]
        public void VerifyRadioButtonInElementsPage()
        {
            log.Info("Verification of radio button in the elements page");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Radio Button");
            Assert.IsFalse(elementsPage.IsNoRadioButtonEnabled(), "The No radio button is enabled");
            elementsPage.ClickOnRadioButton("Impressive");
            string actualRadioButtonOutput = elementsPage.GetRadioButtonSuccessMessage();
            string expectedRadioButtonOutput = "You have selected Impressive";
            Assert.AreEqual(expectedRadioButtonOutput, actualRadioButtonOutput, "Wrong selection for radio button");
            log.Info("Verified radio button in the elements page");
        }

        [Test, Order(7), Category("ElementsPageVerification")]
        public void VerifyDataFieldsInElementsPage()
        {
            log.Info("Verification of data fields in the elements page");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Web Tables");
            WebTableDetails webTableDetails = new()
            {
                FirstName = "Sojan",
                LastName = "Somarajan",
                Age = 28,
                Email = "sojan@email.com",
                Salary = 10000,
                Department = "Testing"
            };
            elementsPage.AddDetailToWebTable(webTableDetails);
            var addedDetailsForAUser = elementsPage.GetAddedDetailsForAUser(webTableDetails.Email);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(addedDetailsForAUser.Contains(webTableDetails.FirstName), "First Name is not present in the web table");
                Assert.IsTrue(addedDetailsForAUser.Contains(webTableDetails.LastName), "Last Name is not present in the web table");
                Assert.IsTrue(addedDetailsForAUser.Contains(webTableDetails.Age.ToString()), "Age is not present in the web table");
                Assert.IsTrue(addedDetailsForAUser.Contains(webTableDetails.Email), "Email Id is not present in the web table");
                Assert.IsTrue(addedDetailsForAUser.Contains(webTableDetails.Salary.ToString()), "Salary is not present in the web table");
                Assert.IsTrue(addedDetailsForAUser.Contains(webTableDetails.Department), "Department is not present in the web table");
            });
            log.Info("Verified data fields in the elements page");
        }

        [Test, Order(8), Category("ElementsPageVerification")]
        public void NavigationVerification()
        {
            log.Info("Verification of Navigation to back");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Text Box");
            var elementsPageUrl = WebDriver!.Url;
            WebDriver.Navigate().Back();
            WebDriver.WaitForPageLoad();
            var homePageUrl = WebDriver.Url;
            Assert.AreNotEqual(elementsPageUrl, homePageUrl);
            log.Info("Verification of Navigation to back completed");
        }
    }
}