using OpenQA.Selenium;
using Core.Extensions;

namespace Application.Utilities
{
    public class UrlUtilities
    {
        private readonly string url = "https://demoqa.com/";
        private readonly IWebDriver webDriver;

        public UrlUtilities(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void NavigateToApplication()
        {
            webDriver.Navigate().GoToUrl(url);
            webDriver.WaitForPageLoad();
        }
    }
}