using PromisesBaseFrameworkTest.Interfaces;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.TestPromiseComponents
{
	public class TestPromiseRequest : GenericRequest, IAmAClaimsRequest
	{
		public string Claim { get; set; }
	}
}
