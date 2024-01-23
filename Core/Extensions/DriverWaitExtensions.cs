using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Core.Extensions
{
    public static class DriverWaitExtensions
    {
        public static void WaitForPageLoad(this IWebDriver driver, int timeoutInSeconds = 60)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
            {
                PollingInterval = TimeSpan.FromSeconds(1),
            };
            wait.Until(PageLoadComplete());
        }

        private static Func<IWebDriver, bool> PageLoadComplete()
        {
            return driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
        }

        public static IAlert WaitUntilAlertIsPresent(this IWebDriver driver)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromMinutes(1))
            {
                PollingInterval = TimeSpan.FromSeconds(1),
            };
            return wait.Until(ExpectedConditions.AlertIsPresent());
        }

        public static void SetTimeOutForImplicitWait(this IWebDriver driver, TimeSpan timeSpan)
        {
            driver.Manage().Timeouts().ImplicitWait = timeSpan;
        }

        public static IWebElement? WaitForAnElementIsVisible(this IWebDriver driver, By by, TimeSpan waitTimeOut = default, TimeSpan pollingInterval = default)
        {
            if (waitTimeOut == default)
            {
                waitTimeOut = TimeSpan.FromMinutes(1);
            }
            if (pollingInterval == default)
            {
                pollingInterval = TimeSpan.FromSeconds(1);
            }
            WebDriverWait wait = new(driver, waitTimeOut)
            {
                PollingInterval = pollingInterval
            };
            try
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IWebElement WaitForAnElementIsVisible(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromMinutes(1))
            {
                PollingInterval = TimeSpan.FromSeconds(1)
            };
            try
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (ElementNotVisibleException exception)
            {
                throw exception;
            }
        }

        public static IWebElement WaitForAnElementIsPresent(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromMinutes(1))
            {
                PollingInterval = TimeSpan.FromSeconds(1)
            };
            try
            {
                return wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (ElementNotVisibleException exception)
            {
                throw exception;
            }
        }

        public static bool IsElementPresent(this IWebDriver driver, By by, TimeSpan waitTimeOut = default, TimeSpan pollingInterval = default)
        {
            if (waitTimeOut == default)
            {
                waitTimeOut = TimeSpan.FromMinutes(1);
            }
            if (pollingInterval == default)
            {
                pollingInterval = TimeSpan.FromSeconds(1);
            }
            WebDriverWait wait = new(driver, waitTimeOut)
            {
                PollingInterval = pollingInterval
            };

            try
            {
                wait.Until(ExpectedConditions.ElementExists(by));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsElementVisible(this IWebDriver driver, By by, TimeSpan waitTimeOut = default, TimeSpan pollingInterval = default)
        {
            if (waitTimeOut == default)
            {
                waitTimeOut = TimeSpan.FromMinutes(1);
            }
            if (pollingInterval == default)
            {
                pollingInterval = TimeSpan.FromSeconds(1);
            }
            WebDriverWait wait = new(driver, waitTimeOut)
            {
                PollingInterval = pollingInterval
            };

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}