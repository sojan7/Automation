using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Utilities.Models;
using Verification.Services;

namespace Verification.Tests.Data_Tests
{
    [TestFixture]
    [Parallelizable]
    internal class DataTest : ITestDataProvider
    {
        private readonly Dictionary<string, string> TestData;

        public DataTest()
        {
            TestData = GetTestData();
        }

        public Dictionary<string, string> GetTestData()
        {
            string jsonContent = File.ReadAllText(GetTestDataPath("TestData\\DataVerificationTestData.json"));
            JObject keyValuePairs = JObject.Parse(jsonContent);
            return keyValuePairs.ToObject<Dictionary<string, string>>()!;
        }

        private string GetTestDataPath(string relativePath)
        {
            return Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, relativePath);
        }

        [Test, Order(1), Category("DataVerification")]
        public void TestEmployeeEquality()
        {
            Employee employee1 = new() { Id = 1, Name = "Sojan", Age = 28 };
            Employee employee2 = new() { Id = 1, Name = "Sojan", Age = 28 };
            Employee employee3 = new() { Id = 2, Name = "John Doe", Age = 25 };
            Assert.AreEqual(employee1, employee2, "Employee1 should be equal to Employee2");
            Assert.AreNotEqual(employee1, employee3, "Employee1 should not be equal to Employee3");
        }
    }
}