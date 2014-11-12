using Microsoft.VisualStudio.TestTools.UnitTesting;
using Termine.Promises;

namespace PromisesBaseTest
{
    [TestClass]
    public class PromiseConstructorTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            var promise = new Promise().WithValidator(f => { return; }).RunAsync();
            
            Assert.IsTrue(promise.ValidatorsCount == 1);
        }

        public void Test
    }
}
