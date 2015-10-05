using NUnit.Framework;
using PromisesBaseFrameworkTest.ComputeTestPromise;
using PromisesBaseFrameworkTest.GetResvPromise;
using PromisesBaseFrameworkTest.TestPromiseComponents;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Handlers;

namespace PromisesBaseFrameworkTest
{
	[TestFixture]
    public class TestPromise
    {
		[Test]
		public void TestPromiseInitializer()
		{
			var promise = ClaimsPromiseFactory.Get<GenericConfig, GenericUserIdentity, TestPromiseW, TestPromiseRq, GenericResponse>();

            promise.WithExecutor("exec", (func =>
		    {
		        func.P.Trace(new GenericEventMessage(func.P.PromiseName));
		        //func.Rx.OutputString = "new output";
		        return Resp.Success();
		    }));

		    promise.WithPromiseExecutor("computeTest", func =>
		    {
		        var config = new PromiseExecutorConfig<ComputeTestRq, ComputeTestRx, GenericUserIdentity>
		        {
		            PromiseFactory = new ComputeTestPf(),
		            Rq = new ComputeTestRq {Multiplier = 5, StartNum = 1},
		            OnResponse = rx =>
		            {
                        func.W.Result = rx.Result;
		            }
		        };

                return config;
		    });

            promise.Run(new PromiseOptions<TestPromiseRq, GenericUserIdentity>(new TestPromiseRq {Name="Joseph", Claim = "1234-Claim"}));

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
            
            promise.WithXferAction("GetSites", func =>
            {
                func.XferConfig.BaseUri = @"http://localhost.fiddler:11368";
                func.XferConfig.EndpointUri = @"/api/1-0/TSWxPaymentService/GetResvById";
                return Resp.Success();
            });

	        //promise.WithPostEnd("testPostEnd", (func =>
         //   {
         //       Assert.IsTrue(func.Rx.ResvEntity != null);
         //       return Resp.Success();
	        //}));

	        promise.Run(new PromiseOptions<GetResvByIdRq, GenericUserIdentity>(new GetResvByIdRq {ResvId = 2}));

            var x = promise.Response;
	    }
    }
}