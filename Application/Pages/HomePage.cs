using OpenQA.Selenium;
using Core.Extensions;
using Utilities.ENUMS;

namespace Application.Pages
{
    public class HomePage
    {
        private readonly IWebDriver webDriver;

        public HomePage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        private string homePageMenus { get; } = "//div[@class= 'card-body']//h5[text()='{0}']";

        public void ClickOnTheHomePageMenu(string menuName)
        {
            IWebElement homePageMenuWebElement = webDriver.WaitForAnElementIsVisible(LocatorType.XPATH.LocateByValue(string.Format(homePageMenus, menuName)))!;
            webDriver.ScrollAnElementIntoView(homePageMenuWebElement!);
            homePageMenuWebElement.Click();
        }

        public List<bool> IsHomePageMenusVisible(List<string> allExpectedMenus)
        {
            List<bool> result = new();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            foreach (string homePageMenu in allExpectedMenus)
            {
                bool isMenuPresent = webDriver.IsElementPresent(LocatorType.XPATH.LocateByValue(string.Format(homePageMenus, homePageMenu)));
                result.Add(isMenuPresent);
            }

            return result;
        }
    }
}