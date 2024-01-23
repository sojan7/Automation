using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Utilities.Utilities;
using Verification.Services;

namespace Verification
{
    public abstract class BaseTest : TestInitialize, ITestDataProvider
    {
        protected IWebDriver? driver = null;
        public Dictionary<string, string> TestData { get; set; }

        protected BaseTest() : base()
        {
            TestData = GetTestData();
        }

        public Dictionary<string, string> GetTestData()
        {
            string jsonContent = File.ReadAllText(GetTestDataPath("TestData\\TestData.json"));
            JObject keyValuePairs = JObject.Parse(jsonContent);
            return keyValuePairs.ToObject<Dictionary<string, string>>()!;
        }

        private string GetTestDataPath(string relativePath)
        {
            return Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, relativePath);
        }
    }
}