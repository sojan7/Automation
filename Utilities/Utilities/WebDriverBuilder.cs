using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Utilities.Utilities;

namespace UI_Utilities.Utilities
{
    public class WebDriverBuilder : Driver
    {
        private string _browserName;
        private bool _maximizeWindow;

        public WebDriverBuilder SetBrowserName(string browserName)
        {
            _browserName = browserName;
            return this;
        }

        public WebDriverBuilder MaximizeWindow(bool maximize)
        {
            if (maximize)
            {
                WebDriver.Manage().Window.Maximize();
            }
            return this;
        }

        public WebDriverBuilder Initilize()
        {
            WebDriver = _browserName.ToLower() switch
            {
                "chrome" => new ChromeDriver(),
                "firefox" => new FirefoxDriver(),
                _ => throw new ArgumentException("Not supported browser"),
            };
            return this;
        }

        public WebDriverBuilder Build()
        {
            return this;
        }
    }
}