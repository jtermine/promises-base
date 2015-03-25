using NUnit.Framework;
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
			var promise = new Promise<GenericConfig, GenericWorkload, GenericRequest, GenericResponse>();
			promise.Run();

			// var configSettings = (PxConfigSection)ConfigurationManager.GetSection("PxConfig");

			Assert.IsTrue(promise.Secret == "12345");

			/*
			foreach (var configElement in configSettings.ConfigElements.AsEnumerable())
			{
				Console.WriteLine(configElement.Key);
				
				foreach (var l_subElement in configElement.SubElements.AsEnumerable())
				{
					Console.WriteLine(l_subElement.Key);
				}

			}
			*/

		}
    }
}