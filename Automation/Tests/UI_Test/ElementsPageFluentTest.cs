using Application.Pages;
using Application.Utilities;
using Core.Extensions;
using FluentAssertions;
using log4net;
using NUnit.Framework;
using Utilities.Models;
using Utilities.Utilities;
using Verification.Tests.TestHelpers;

namespace Verification.Tests.UI_Test
{
    [TestFixture]
    [Parallelizable]
    public class ElementsPageFluentTest : BaseTest
    {
        private readonly UrlUtilities urlUtilities;
        private readonly HomePage homePage;
        private readonly ElementsPage elementsPage;
        private readonly ILog log;

        public ElementsPageFluentTest()
        {
            TestLogger.InitializeLog();
            log = LogManager.GetLogger(typeof(ElementsPageFluentTest));
            driver = GetDriver(TestContext.Parameters["Browser"]!);
            urlUtilities = new(driver);
            homePage = new(driver);
            elementsPage = new(driver);
        }

        [SetUp]
        public void ElementsPageFluentTestSetUp()
        {
            urlUtilities.NavigateToApplication();
        }

        [TearDown]
        public void ElementsPageFluentTestTearDown()
        {
            driver!.CaptureScreenShotForFailedTests();
        }

        [OneTimeTearDown]
        public void ElementsPageFluentTestOneTimeTearDown()
        {
            driver!.QuitActiveDriverInstance();
        }

        [Test, Order(1), Category("FluentAssertions")]
        public void VerifyAllExpectedMenusArePresentInHomePage()
        {
            log.Info("Verification that all expected menus are displayed on the homepage");

            List<string> expectedMenus = new()
            {
            "Elements",
            "Forms",
            "Alerts, Frame & Windows",
            "Widgets",
            "Interactions",
            "Book Store Application"
            };

            var allHomePageMenuVisibility = homePage.IsHomePageMenusVisible(expectedMenus);
            allHomePageMenuVisibility.Should().OnlyContain(item => item, "All the expected menus should be present in the UI");
            log.Info("Verified that all expected menus are displayed on the homepage");
        }

        [Test, Order(2), Category("FluentAssertions")]
        public void VerifyExceptionCaseForElementsPage()
        {
            log.Info("Verification that Text Bx is not displayed");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.IsASpecificMenuVisbleInUi("Text Bx").Should().BeFalse("Text Bx should not be displayed in the UI");
            log.Info("Verified that Text Bx is not displayed");
        }

        [Test, Order(3), Category("FluentAssertions")]
        public void VerifyErrorForInvalidEmailInTextBoxElementsPage()
        {
            log.Info("Verification of invalid email in the elements page");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Text Box");
            elementsPage.GetVisibilityOfInvalidEmailError("sojan").Should().BeTrue("Error should be shown when an invalid email id is entered");
            log.Info("Verified invalid email in the elements page completed");
        }

        [Test, Order(4), Category("FluentAssertions")]
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
            elementsPage.GetTextBoxOutPutText().Should().Be("Name:Sojan Somarajan\r\nEmail:sojan@epam.com\r\nCurrent Address :Current address Testing new line\r\nPermananet Address :Permanent address Testing new line");
            log.Info("Verified user details fill in the elements page");
        }

        [Test, Order(5), Category("FluentAssertions")]
        public void VerifyCheckBoxInElementsPage()
        {
            log.Info("Verification of check box in the elements page");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Check Box");
            elementsPage.SelectWordFileCheckBox();
            elementsPage.GetCheckBoxResult().Should().Be("You have selected :\r\nwordFile");
            log.Info("Verified check box in the elements page");
        }

        [Test, Order(6), Category("FluentAssertions")]
        public void VerifyRadioButtonInElementsPage()
        {
            log.Info("Verification of radio button in the elements page");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Radio Button");
            elementsPage.IsNoRadioButtonEnabled().Should().BeFalse("The No radio button should not be enabled");
            elementsPage.ClickOnRadioButton("Impressive");
            elementsPage.GetRadioButtonSuccessMessage().Should().Be("You have selected Impressive", "Wrong selection for radio button");
            log.Info("Verified radio button in the elements page");
        }

        [Test, Order(7), Category("FluentAssertions")]
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
            addedDetailsForAUser.Should().Contain(webTableDetails.FirstName, "First Name is not present in the web table");
            addedDetailsForAUser.Should().Contain(webTableDetails.LastName, "Last Name is not present in the web table");
            addedDetailsForAUser.Should().Contain(webTableDetails.Age.ToString(), "Age is not present in the web table");
            addedDetailsForAUser.Should().Contain(webTableDetails.Email, "Email Id is not present in the web table");
            addedDetailsForAUser.Should().Contain(webTableDetails.Salary.ToString(), "Salary is not present in the web table");
            addedDetailsForAUser.Should().Contain(webTableDetails.Department, "Department is not present in the web table");
            log.Info("Verified data fields in the elements page");
        }

        [Test, Order(8), Category("FluentAssertions")]
        public void NavigationVerification()
        {
            log.Info("Verification of Navigation to back");
            homePage.ClickOnTheHomePageMenu("Elements");
            elementsPage.ClickOnElementsPageSideMenu("Text Box");
            var elementsPageUrl = WebDriver!.Url;
            WebDriver.Navigate().Back();
            WebDriver.WaitForPageLoad();
            var homePageUrl = WebDriver.Url;
            elementsPageUrl.Should().NotBe(homePageUrl, "Navigation back should take you to a different page");
            log.Info("Verification of Navigation to back completed");
        }
    }
}