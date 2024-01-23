using OpenQA.Selenium;

namespace Core.Extensions
{
    public static class JavaScriptExtensions
    {
        public static IJavaScriptExecutor Script(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }
    }
}