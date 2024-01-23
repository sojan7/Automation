namespace Verification.Services
{
    internal interface ITestDataProvider
    {
        Dictionary<string, string> GetTestData();
    }
}