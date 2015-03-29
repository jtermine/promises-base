using Termine.Promises.Base.Interfaces;

namespace PromisesBaseFrameworkTest.Interfaces
{
	public interface IAmAClaimsRequest : IAmAPromiseRequest
	{
		 string Claim { get; set; }
	}
}