using NUnit.Framework;
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
			var promise = ClaimsPromiseFactory.Get<GenericConfig, GenericUserIdentity, GenericWorkload, TestPromiseRequest, TestPromiseResponse>();

			promise.WithAuthChallenger("auth", (p, c, u, w, rq, rx) =>
			{
				rx.ResponseCode = 200;
			});

			promise.WithExecutor("exec", (p, c, u, w, rq, rx) =>
			{
				p.Trace(new GenericEventMessage(p.PromiseName));
				rx.OutputString = "new output";
                
			});


			promise.Run();

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
            var getSitesPromise = new Promise<GenericConfig, GenericUserIdentity, GenericWorkload, GetSitesRequest, GetSitesResponse>(true);
            
            getSitesPromise.WithXferAction("GetSites", (config, p, c, u, w, rq, rx) =>
            {
                config.BaseUri = @"http://localhost.fiddler/api/1.0/testService";
                config.EndpointUri = @"/GetSites";
            });

	        getSitesPromise.WithPostEnd("testPostEnd", (p, c, u, w, rq, rx) =>
	        {
                Assert.IsTrue(rx.Sites.Count > 0);
	        });
            
            getSitesPromise.Run();
        }
    }
}