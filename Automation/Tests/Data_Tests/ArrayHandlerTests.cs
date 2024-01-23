using NUnit.Framework;
using Utilities.Utilities;

namespace Verification.Tests.Data_Tests
{
    [TestFixture]
    public class ArrayHandlerTests
    {
        [Test, Order(1), Category("ArrayVerification")]
        public void TestContainsElement()
        {
            int[] testArray = { 1, 2, 3, 4, 5, 6 };
            ArrayHandler arrayHandler = new(testArray);
            Assert.IsTrue(arrayHandler.ContainsElement(5));
            Assert.IsFalse(arrayHandler.ContainsElement(10));
        }

        [Test, Order(2), Category("ArrayVerification")]
        public void TestSortArray()
        {
            int[] testArray = { 9, 8, 7, 6, 5 };
            ArrayHandler arrayHandler = new(testArray);
            int[] sortedArray = arrayHandler.SortArray();
            Array.Sort(testArray);
            Assert.AreEqual(testArray, sortedArray);
        }
    }
}