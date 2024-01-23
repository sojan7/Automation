using OpenQA.Selenium;

namespace Utilities.ENUMS
{
    public enum LocatorType
    {
        ID,
        NAME,
        CLASS_NAME,
        TAG_NAME,
        LINK_TEXT,
        PARTIAL_LINK_TEXT,
        XPATH,
        CSS_SELECTOR
    }

    public static class LocateBy
    {
        public static By LocateByValue(this LocatorType locatorType, string value)
        {
            return locatorType switch
            {
                LocatorType.ID => By.Id(value),
                LocatorType.NAME => By.Name(value),
                LocatorType.CLASS_NAME => By.ClassName(value),
                LocatorType.TAG_NAME => By.TagName(value),
                LocatorType.LINK_TEXT => By.LinkText(value),
                LocatorType.PARTIAL_LINK_TEXT => By.PartialLinkText(value),
                LocatorType.XPATH => By.XPath(value),
                LocatorType.CSS_SELECTOR => By.CssSelector(value),
                _ => throw new ArgumentOutOfRangeException(nameof(LocatorType), "Unsupported locator type"),
            };
        }
    }
}