using NUnit.Framework;
using PromisesBaseFrameworkTest.GetResvPromise;
using PromisesBaseFrameworkTest.GetSitesPromise;
using PromisesBaseFrameworkTest.TestPromiseComponents;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest
{
	[TestFixture]
    public class TestPromise
    {
		[Test]
		public void TestPromiseInitializer()
		{
			var promise = ClaimsPromiseFactory.Get<GenericConfig, GenericUserIdentity, GenericWorkload, TestPromiseRequest, GenericResponse>();

            promise.WithExecutor("exec", (func =>
		    {
		        func.P.Trace(new GenericEventMessage(func.P.PromiseName));
		        //func.Rx.OutputString = "new output";
		        return Resp.Success();
		    }));

		    promise.Run(
		        new PromiseOptions<TestPromiseRequest, GenericUserIdentity>(new TestPromiseRequest {Claim = "1234-Claim"}));

            promise.Abort(new GenericEventMessage("Abort promise-1"));

            promise.Run();

            promise.Abort(new GenericEventMessage("Abort promise-2"));
            
            promise.Run();

            promise.Abort(new GenericEventMessage("Abort promise-3"));
            
            promise.Run();

            var response = promise.SerializeResponse();

			Assert.IsNotEmpty(response);

		}

	    [Test]
	    public void TestXfer()
	    {
            var promise = new Promise<GenericConfig, GenericUserIdentity, GenericWorkload, GetResvByIdRq, GetResvByIdRx>(true);
            
            promise.WithXferAction("GetSites", (config, p, c, u, w, rq, rx) =>
            {
                config.BaseUri = @"http://localhost.fiddler:11368";
                config.EndpointUri = @"/api/1-0/TSWxPaymentService/GetResvById";
            });

	        promise.WithPostEnd("testPostEnd", (func =>
            {
                Assert.IsTrue(func.Rx.ResvEntity != null);
                return Resp.Success();
	        }));

	        promise.Run(new PromiseOptions<GetResvByIdRq, GenericUserIdentity>(new GetResvByIdRq {ResvId = 2}));
	    }
    }
}