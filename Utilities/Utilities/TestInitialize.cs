using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Utilities.Utilities
{
    public class TestInitialize : Driver
    {
        /// <summary>
        /// Method will generata an instance of driver accorind to the browser requirments
        /// </summary>
        /// <param name="browserName">Chrome or Firefox</param>
        /// <returns>An instance of IWebDriver</returns>
        /// <exception cref="ArgumentException">Wrong bowser selection, No implementation for browsers other than chrome and firefox</exception>
        public IWebDriver GetDriver(string browserName)
        {
            WebDriver = browserName.ToLower() switch
            {
                "chrome" => new ChromeDriver(),
                "firefox" => new FirefoxDriver(),
                _ => throw new CustomExceptions("Not supported browser"),
            };
            if (WebDriver is not null)
            {
                WebDriver!.Manage().Window.Maximize();
            }
            else
            {
                throw new InvalidOperationException("Failed to initialize the driver.");
            }

            return WebDriver;
        }
    }
}