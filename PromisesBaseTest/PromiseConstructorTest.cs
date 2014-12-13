using System.IO;
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
                new Promise<ClaimsBasedWorkload>();

            promise.WithValidator(new PromiseActionInstance<ClaimsBasedWorkload>("1",
                    workload =>
                    {
                        workload.IsTerminated = true;
                    }));

            promise.Run();

            Assert.IsTrue(promise.ValidatorsCount == 1, "The promise has [{0}] validators attached when it expected [1] ", promise.ValidatorsCount);
        }

        [TestMethod]    
        public void TestCreateLockPromise()
        {
            var promise = new CreateLockPromise();
            promise.Run();

            Assert.IsTrue(promise.AuthChallengerChecksum == 1, "AuthChallengerChecksum equaled [{0}] when it should have been [1].", promise.AuthChallengerChecksum);
            Assert.IsTrue(promise.ValidatorChecksum == 2, "ValidatorCheckSum equaled [{0}] when it should have been [2].", promise.ValidatorChecksum);
            Assert.IsTrue(promise.ExecutorChecksum == 3, "ExecutorChecksum equaled [{0}] when it should have been [3].", promise.ExecutorChecksum);
        }

        [TestMethod]
        public void TestFalseLockPromise()
        {
            var promise = new FalseLockPromise();
            promise.Run();

            Assert.IsTrue(promise.AuthChallengerChecksum == 1, "AuthChallengerChecksum equaled [{0}] when it should have been [1].", promise.AuthChallengerChecksum);
            Assert.IsTrue(promise.ValidatorChecksum == 2, "ValidatorCheckSum equaled [{0}] when it should have been [2].", promise.ValidatorChecksum);
            Assert.IsTrue(promise.ExecutorChecksum == 0, "ExecutorChecksum equaled [{0}] when it should have been [0].", promise.ExecutorChecksum);
        }

        [TestMethod]
        public void TestClaimsBasedAuthBase()
        {
            var testClaimsPromise = new ClaimsBasedPromise();

            testClaimsPromise
                .WithNlogInstrumentation()
                .WithDefaultClaimsBasedAuth();

            testClaimsPromise.Workload.Request = new ClaimsBasedRequest {Claim = "1234"};
            
            testClaimsPromise.Run();
            
            Assert.IsTrue(testClaimsPromise.Workload.IsTerminated);
        }

        [TestMethod]
        public void TestDuplicatePromises()
        {
            var promiseA = new FalseLockPromise();

            promiseA
                .WithDuplicatePrevention()
                .WithRequestId("1234");

            promiseA.Run();

            var promiseB = new FalseLockPromise();

            promiseB
                .WithDuplicatePrevention()
                .WithRequestId("1234");

            promiseB.Run();

            Assert.IsTrue(promiseB.Workload.IsBlocked);
        }

        [TestMethod]
        public void TestSerialization()
        {
            var promise = new ClaimsBasedPromise();

            promise.WithRequestId("12345");

            promise.Workload.Request.Init(promise.Workload.RequestId);

            promise.Workload.Request.Claim = "4444";

            var testStream = new MemoryStream();

            promise.Workload.Request.Serialize(testStream);

            testStream.Flush();
            testStream.Seek(0, SeekOrigin.Begin);

            Assert.IsTrue(testStream.Capacity > 0);

            var request = testStream.Deserialize<ClaimsBasedRequest>();

            Assert.IsTrue(request.RequestName == typeof(ClaimsBasedRequest).FullName);
            Assert.IsTrue(request.RequestId == promise.Workload.RequestId);
            Assert.IsTrue(request.Claim == "4444");
        }
    }
}