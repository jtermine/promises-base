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

			var response = promise.SerializeResponse();

			Assert.IsNotEmpty(response);

		}
    }
}