using Microsoft.VisualStudio.TestTools.UnitTesting;
using Termine.Promises.Base.Test.TestObjects;
using Termine.Promises.Base.Test.TestPromises;

namespace Termine.Promises.Base.Test
{
    [TestClass]
    public class PromiseConstructorTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            var promise =
                new Promise<PromiseWorkload, PromiseRequest, PromiseResponse>().WithValidator(
                    new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("1", workload =>
                    {
                        workload.Request.Claim = "1";
                    }));

            Assert.IsTrue(promise.ValidatorsCount == 1);
        }

        [TestMethod]
        public void TestCreateLockPromise()
        {
            var promise = new CreateLockPromise();
            promise.RunAsync();

            Assert.IsTrue(promise.AuthChallengerChecksum == 1, "AuthChallengerChecksum equaled [{0}] when it should have been [1].", promise.AuthChallengerChecksum);
            Assert.IsTrue(promise.ValidatorChecksum == 2, "ValidatorCheckSum equaled [{0}] when it should have been [2].", promise.ValidatorChecksum);
            Assert.IsTrue(promise.ExecutorChecksum == 3, "ExecutorChecksum equaled [{0}] when it should have been [3].", promise.ExecutorChecksum);
        }
    }
}
