using Microsoft.VisualStudio.TestTools.UnitTesting;
using Termine.Promises.Base.Test.ClaimsBasePromiseObjects;
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
                new Promise<ClaimsBasedWorkload>().WithValidator(new PromiseActionInstance<ClaimsBasedWorkload>("1",
                    workload =>
                    {
                        workload.TerminateProcessing = true;
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

        [TestMethod]
        public void TestFalseLockPromise()
        {
            var promise = new FalseLockPromise();
            promise.RunAsync();

            Assert.IsTrue(promise.AuthChallengerChecksum == 1, "AuthChallengerChecksum equaled [{0}] when it should have been [1].", promise.AuthChallengerChecksum);
            Assert.IsTrue(promise.ValidatorChecksum == 2, "ValidatorCheckSum equaled [{0}] when it should have been [2].", promise.ValidatorChecksum);
            Assert.IsTrue(promise.ExecutorChecksum == 0, "ExecutorChecksum equaled [{0}] when it should have been [0].", promise.ExecutorChecksum);
        }

        [TestMethod]
        public void TestClaimsBasedAuthBase()
        {
            var testClaimsPromise = new ClaimsBasedPromise();

            testClaimsPromise.Workload.Request = new ClaimsBasedRequest {Claim = "1234"};
            
            testClaimsPromise.RunAsync();
            
            Assert.IsTrue(testClaimsPromise.Workload.TerminateProcessing);
        }
    }
}