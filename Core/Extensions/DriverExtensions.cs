using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Core.Extensions
{
    public static class DriverExtensions
    {
        public static void QuitActiveDriverInstance(this IWebDriver driver)
        {
            driver.Quit();
        }

        public static void ScrollAnElementIntoView(this IWebDriver driver, IWebElement webElement)
        {
            driver.Script().ExecuteScript("arguments[0].scrollIntoView(arguments[1]);", webElement, true);
        }

        public static void ScrollToTop(this IWebDriver driver)
        {
            driver.Script().ExecuteScript("window.scrollTo(0, 0);");
        }

        public static void ScrollToBottom(this IWebDriver driver)
        {
            driver.Script().ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        public static string GetAlertMessage(this IWebDriver driver)
        {
            driver.WaitUntilAlertIsPresent();
            var alert = driver.SwitchTo().Alert();
            return alert.Text;
        }

        public static void AcceptAlert(this IWebDriver driver)
        {
            driver.WaitUntilAlertIsPresent();
            var alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public static void DismissAlert(this IWebDriver driver)
        {
            driver.WaitUntilAlertIsPresent();
            var alert = driver.SwitchTo().Alert();
            alert.Dismiss();
        }

        public static void EnterValueInAlert(this IWebDriver driver, string enterValue)
        {
            driver.WaitUntilAlertIsPresent();
            var alert = driver.SwitchTo().Alert();
            alert.SendKeys(enterValue);
        }

        public static void MouseHover(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new(driver);
            actions.MoveToElement(element).Perform();
        }

        public static void MouseClickAndHold(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new(driver);
            actions.ClickAndHold(element).Perform();
        }

        public static void MouseClickAndHold(this IWebDriver driver)
        {
            Actions actions = new(driver);
            actions.ClickAndHold().Perform();
        }

        public static void MouseClick(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new(driver);
            actions.Click(element).Perform();
        }

        public static void MouseRightClick(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new(driver);
            actions.ContextClick(element).Perform();
        }

        public static void MouseRightClick(this IWebDriver driver)
        {
            Actions actions = new(driver);
            actions.ContextClick().Perform();
        }

        public static void MouseDoubleClick(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new(driver);
            actions.DoubleClick(element).Perform();
        }

        public static void MoveToAnElementRightClickAndDoubleClick(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new(driver);
            actions.MoveToElement(element).ContextClick(element).DoubleClick(element).Build().Perform();
        }

        public static void MouseDragAndDrop(this IWebDriver driver, IWebElement source, IWebElement destination)
        {
            Actions actions = new(driver);
            actions.DragAndDrop(source, destination).Build().Perform();
        }

        public static void MouseDragAndDrop(this IWebDriver driver, IWebElement elementToBeDragged, int sourceX, int destinationY)
        {
            Actions actions = new(driver);
            actions.DragAndDropToOffset(elementToBeDragged, sourceX, destinationY).Perform();
        }

        public static void CaptureScreenShot(this IWebDriver driver, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath);
        }
    }
}