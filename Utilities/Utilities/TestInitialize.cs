using OpenQA.Selenium;
using UI_Utilities.Utilities;

namespace Utilities.Utilities
{
    public class TestInitialize : WebDriverBuilder
    {
        /// <summary>
        /// Method will generata an instance of driver accorind to the browser requirments
        /// </summary>
        /// <param name="browserName">Chrome or Firefox</param>
        /// <returns>An instance of IWebDriver</returns>
        /// <exception cref="ArgumentException">Wrong bowser selection, No implementation for browsers other than chrome and firefox</exception>
        public IWebDriver GetDriver(string browserName)
        {
            return new WebDriverBuilder()
            .SetBrowserName(browserName)
            .Initilize()
            .MaximizeWindow(true)
            .Build().WebDriver!;
        }
    }
}