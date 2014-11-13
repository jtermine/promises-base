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
                    new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("1", (p, workload) =>
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
        public void TestJoinPromises()
        {
            var createLockPromise = new CreateLockPromise();
            var falseLockPromise = new FalseLockPromise();

            var joinedPromise = CreateLockPromise.Join(createLockPromise, falseLockPromise);
            
            Assert.IsTrue(joinedPromise.AuthChallengersCount == 2, "AuthChallengersCount was expected to be [2] but was [{0}]", joinedPromise.AuthChallengersCount);
            Assert.IsTrue(joinedPromise.ValidatorsCount == 2, "ValidatorsCount was expected to be [2] but was [{0}]", joinedPromise.ValidatorsCount);
            Assert.IsTrue(joinedPromise.ExecutorsCount == 2, "ExecutorsCount was expected to be [2] but was [{0}]", joinedPromise.ExecutorsCount);
            
            joinedPromise.RunAsync();

            Assert.IsTrue(joinedPromise.AuthChallengerChecksum == 1, "AuthChallengerChecksum equaled [{0}] when it should have been [1].", joinedPromise.AuthChallengerChecksum);
            Assert.IsTrue(joinedPromise.ValidatorChecksum == 2, "ValidatorCheckSum equaled [{0}] when it should have been [2].", joinedPromise.ValidatorChecksum);
            Assert.IsTrue(joinedPromise.ExecutorChecksum == 0, "ExecutorChecksum equaled [{0}] when it should have been [0].", joinedPromise.ExecutorChecksum);
        }
    }
}