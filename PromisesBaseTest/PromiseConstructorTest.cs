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
            var promise = new Promise().WithAuthChallenger(f => { return; }).RunAsync();
            Assert.IsNotNull(promise);
        }
    }
}
