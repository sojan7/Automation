using Core.Extensions;
using OpenQA.Selenium;
using Utilities.ENUMS;
using Utilities.Models;

namespace Application.Pages
{
    public class ElementsPage
    {
        private readonly IWebDriver webDriver;

        public ElementsPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        #region Elements

        private By UserFullNameTextFieldLocator { get; } = By.Id("userName");
        private By UserEmailTextFieldLocator { get; } = By.Id("userEmail");
        private By TextBoxSubmitButtonLocator { get; } = By.CssSelector(".btn.btn-primary");
        private By UserEmailTextFieldErrorIndicatorLocator { get; } = By.XPath("//input[@class='mr-sm-2 field-error form-control']");
        private By CurrentAddressLocator { get; } = By.Id("currentAddress");
        private By PermanentAddressLocator { get; } = By.Id("permanentAddress");
        private By TextBoxOutputLocator { get; } = By.Id("output");
        private By HomeCheckBoxExpandButtonLocator { get; } = By.XPath("//label[@for='tree-node-home']/parent::span//button");
        private By DownloadsCheckBoxExpandButtonLocator { get; } = By.XPath("//label[@for='tree-node-downloads']/parent::span//button");
        private By WordFileCheckBoxLocator { get; } = By.XPath("//span[contains(text(),'Word File.doc')]");
        private By CheckBoxOutputLocator { get; } = By.XPath("//div[@id='result']");
        private By NoRadioButtonLocator { get; } = By.Id("noRadio");
        private By YesRadioButtonLocator { get; } = By.XPath("//label[text()='Yes']");
        private By ImpressiveRadioButtonLocator { get; } = By.XPath("//label[text()='Impressive']");
        private By RadioButtonSuccessMessageLocator { get; } = By.ClassName("mt-3");
        private By WebTableAddRecordButtonLocator { get; } = By.Id("addNewRecordButton");
        private By WebTableFirstNameFieldLocator { get; } = By.XPath("//input[@placeholder='First Name']");
        private By WebTableLastNameFieldLocator { get; } = By.XPath("//input[@placeholder='Last Name']");
        private By WebTableEmailFieldLocator { get; } = By.XPath("//input[@placeholder='name@example.com']");
        private By WebTableAgeFieldLocator { get; } = By.XPath("//input[@placeholder='Age']");
        private By WebTableSalaryFieldLocator { get; } = By.XPath("//input[@placeholder='Salary']");
        private By WebTableDepartMentFieldLocator { get; } = By.XPath("//input[@placeholder='Department']");
        private By WebTableSubmitButtonLocator { get; } = By.Id("submit");

        #endregion Elements

        private string elementsPageLeftMenu { get; } = "//li[@class='btn btn-light ']//span[text()='replacer']";
        private string webTableDetailsForAParticularUser { get; } = "//div[@class='rt-td' and contains(text(),'replacer')]/parent::div";

        public bool IsASpecificMenuVisbleInUi(string menuName)
        {
            try
            {
                IWebElement homePageMenuWebElement = webDriver.WaitForAnElementIsVisible(LocatorType.XPATH.LocateByValue(elementsPageLeftMenu.Replace("replacer", menuName)), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1))!;
                return homePageMenuWebElement is not null;
            }
            finally
            {
                Console.WriteLine($"The {menuName} menu verification in elements page completed");
            }
        }

        public void ClickOnElementsPageSideMenu(string menuName)
        {
            webDriver.WaitForAnElementIsVisible(LocatorType.XPATH.LocateByValue(elementsPageLeftMenu.Replace("replacer", menuName))).Click();
            webDriver.WaitForPageLoad();
        }

        public void FillUserFullName(string userFullName)
        {
            webDriver.WaitForAnElementIsVisible(UserFullNameTextFieldLocator).SendKeys(userFullName);
        }

        public void FillUserEmailId(string emailId)
        {
            webDriver.WaitForAnElementIsVisible(UserEmailTextFieldLocator).SendKeys(emailId);
        }

        public void ClickOnTextBoxSubmitButton()
        {
            webDriver.ScrollAnElementIntoView(webDriver.WaitForAnElementIsPresent(TextBoxSubmitButtonLocator));
            webDriver.WaitForAnElementIsVisible(TextBoxSubmitButtonLocator).Click();
        }

        public bool GetVisibilityOfInvalidEmailError(string invalidEmailId)
        {
            FillUserEmailId(invalidEmailId);
            ClickOnTextBoxSubmitButton();
            webDriver.ScrollToTop();
            return webDriver.WaitForAnElementIsVisible(UserEmailTextFieldErrorIndicatorLocator).Displayed;
        }

        public void EnterCurrentAddress(string currentAddress)
        {
            webDriver.ScrollAnElementIntoView(webDriver.WaitForAnElementIsPresent(CurrentAddressLocator));
            webDriver.WaitForAnElementIsPresent(CurrentAddressLocator).SendKeys(currentAddress);
        }

        public void EnterPermanentAddress(string permanentAddress)
        {
            webDriver.ScrollAnElementIntoView(webDriver.WaitForAnElementIsPresent(PermanentAddressLocator));
            webDriver.WaitForAnElementIsPresent(PermanentAddressLocator).SendKeys(permanentAddress);
        }

        public string GetTextBoxOutPutText()
        {
            return webDriver.WaitForAnElementIsPresent(TextBoxOutputLocator).Text;
        }

        public void SelectWordFileCheckBox()
        {
            webDriver.WaitForAnElementIsVisible(HomeCheckBoxExpandButtonLocator).Click();
            webDriver.WaitForAnElementIsVisible(DownloadsCheckBoxExpandButtonLocator).Click();
            webDriver.WaitForAnElementIsVisible(WordFileCheckBoxLocator).Click();
        }

        public string GetCheckBoxResult()
        {
            return webDriver.WaitForAnElementIsVisible(CheckBoxOutputLocator).Text;
        }

        public bool IsNoRadioButtonEnabled()
        {
            return webDriver.WaitForAnElementIsPresent(NoRadioButtonLocator).Enabled;
        }

        public void ClickOnRadioButton(string buttonName)
        {
            switch (buttonName.ToLower())
            {
                case "yes":
                    webDriver.WaitForAnElementIsPresent(YesRadioButtonLocator).Click();
                    break;

                case "impressive":
                    webDriver.WaitForAnElementIsPresent(ImpressiveRadioButtonLocator).Click();
                    break;

                default:
                    throw new Exception("Invalid radio button");
            }
        }

        public string GetRadioButtonSuccessMessage()
        {
            return webDriver.WaitForAnElementIsVisible(RadioButtonSuccessMessageLocator).Text;
        }

        public void AddDetailToWebTable(WebTableDetails webTableDetails)
        {
            webDriver.WaitForAnElementIsVisible(WebTableAddRecordButtonLocator).Click();
            webDriver.WaitForAnElementIsVisible(WebTableFirstNameFieldLocator).SendKeys(webTableDetails.FirstName);
            webDriver.WaitForAnElementIsVisible(WebTableLastNameFieldLocator).SendKeys(webTableDetails.LastName);
            webDriver.WaitForAnElementIsVisible(WebTableEmailFieldLocator).SendKeys(webTableDetails.Email);
            webDriver.WaitForAnElementIsVisible(WebTableAgeFieldLocator).SendKeys(webTableDetails.Age.ToString());
            webDriver.WaitForAnElementIsVisible(WebTableSalaryFieldLocator).SendKeys(webTableDetails.Salary.ToString());
            webDriver.WaitForAnElementIsVisible(WebTableDepartMentFieldLocator).SendKeys(webTableDetails.Department);
            webDriver.WaitForAnElementIsVisible(WebTableSubmitButtonLocator).Click();
        }

        public string GetAddedDetailsForAUser(string addedEmailId)
        {
            return webDriver.WaitForAnElementIsVisible(LocatorType.XPATH.LocateByValue(webTableDetailsForAParticularUser.Replace("replacer", addedEmailId))).Text;
        }
    }
}