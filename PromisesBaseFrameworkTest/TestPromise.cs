using NUnit.Framework;
using PromisesBaseFrameworkTest.TestPromiseComponents;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest
{
	[TestFixture]
    public class TestPromise
    {
		[Test]
		public void TestPromiseInitializer()
		{
			var promise = ClaimsPromiseFactory.Get<GenericConfig, GenericWorkload, TestPromiseRequest, TestPromiseResponse>();

			promise.WithAuthChallenger("auth", (p, c, w, rq, rx) =>
			{
				rx.ResponseCode = 200;
			});

			promise.WithExecutor("exec", (p, c, w, rq, rx) =>
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
    }
}