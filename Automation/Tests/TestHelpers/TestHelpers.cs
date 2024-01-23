using Core.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Verification.Tests.TestHelpers
{
    public static class TestHelpers
    {
        public static void CaptureScreenShotForFailedTests(this IWebDriver driver)
        {
            if (!TestContext.CurrentContext.Result.Outcome.Status.ToString().Equals("Passed"))
            {
                string filePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, $"Tests\\Screenshots\\{TestContext.CurrentContext.Test.Name}_{DateTime.Now:dd_MM_yyyy_HH_MM}.png");
                driver!.CaptureScreenShot(filePath);
            }
        }

        public static void DeleteAllExistingScreenShots()
        {
            var a = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, $"Tests\\Screenshots");
            string[] files = Directory.GetFiles(a);
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }
    }
}