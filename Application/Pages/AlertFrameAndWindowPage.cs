using Core.Extensions;
using OpenQA.Selenium;

namespace Application.Pages
{
    public class AlertFrameAndWindowPage
    {
        private readonly IWebDriver webDriver;

        #region Elements

        private By SimpleAlertButtonLocator { get; } = By.Id("alertButton");
        private By TimedAlertButtonLocator { get; } = By.Id("timerAlertButton");
        private By AlertButtonWithConfirmButtonLocator { get; } = By.Id("confirmButton");
        private By AlertButtonWithPromtLocator { get; } = By.Id("promtButton");
        private By ConfirmResultLocator { get; } = By.Id("confirmResult");
        private By FrameSampleHeading { get; } = By.XPath("//h1[@id='sampleHeading']");
        private By ShowSmallDialogModelLocator { get; } = By.Id("showSmallModal");
        private By ShowLargeDialogModelLocator { get; } = By.Id("showLargeModal");
        private By DialogTextLocator { get; } = By.ClassName("modal-body");
        private By ModelDialogCloseButton { get; } = By.XPath("//div[@class='modal-footer']//button[@type='button']");
        private By NewTabButtonLocator { get; } = By.Id("tabButton");
        private By NewWindowPageSampleHeadingLocator { get; } = By.Id("sampleHeading");

        #endregion Elements

        public AlertFrameAndWindowPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        private string AlertPageSideMenu { get; } = "//span[text()='replacer']";

        public string GetNewWindowPageSampleHeading()
        {
            return webDriver.WaitForAnElementIsVisible(NewWindowPageSampleHeadingLocator).Text;
        }

        public void ClickOnNewTab()
        {
            webDriver.WaitForAnElementIsVisible(NewTabButtonLocator).Click();
        }

        public void ClickOnAlertPageSideMenu(string sideMenuToBeClicked)
        {
            webDriver.ScrollAnElementIntoView(webDriver.WaitForAnElementIsPresent(By.XPath(AlertPageSideMenu.Replace("replacer", sideMenuToBeClicked))));
            webDriver.WaitForAnElementIsVisible(By.XPath(AlertPageSideMenu.Replace("replacer", sideMenuToBeClicked))).Click();
            webDriver.WaitForPageLoad();
        }

        public void ClickOnSimpleAlertButton()
        {
            webDriver.WaitForAnElementIsVisible(SimpleAlertButtonLocator).Click();
        }

        public void ClickOnDelayedAlertButton()
        {
            webDriver.WaitForAnElementIsVisible(TimedAlertButtonLocator).Click();
        }

        public void ClickOnAlertButtonWithConfirmButton()
        {
            webDriver.WaitForAnElementIsVisible(AlertButtonWithConfirmButtonLocator).Click();
        }

        public void ClickOnAlertButtonWithPromtButton()
        {
            webDriver.WaitForAnElementIsVisible(AlertButtonWithPromtLocator).Click();
        }

        public string GetConfirmAlertResultText()
        {
            return webDriver.WaitForAnElementIsVisible(ConfirmResultLocator).Text;
        }

        public string GetTextFromFrame()
        {
            webDriver.SwitchTo().Frame("frame1");
            var frameElement = webDriver.WaitForAnElementIsVisible(FrameSampleHeading);
            var frameElementText = frameElement.Text;
            webDriver.SwitchTo().DefaultContent();
            return frameElementText;
        }

        public void ClickOnSmallModelButton()
        {
            webDriver.WaitForAnElementIsVisible(ShowSmallDialogModelLocator).Click();
        }

        public void ClickOnLargeModelButton()
        {
            webDriver.WaitForAnElementIsVisible(ShowLargeDialogModelLocator).Click();
        }

        public string GetTextFromDialogBox()
        {
            return webDriver.WaitForAnElementIsVisible(DialogTextLocator).Text;
        }

        public void CloseDialogBox()
        {
            webDriver.WaitForAnElementIsVisible(ModelDialogCloseButton).Click();
        }
    }
}