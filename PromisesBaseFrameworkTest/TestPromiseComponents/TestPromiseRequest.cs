using FluentValidation;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.TestPromiseComponents
{
	public class TestPromiseRequest : GenericRequest
	{
	    public string Name { get; set; }
		public string Claim { get; set; }

	    public override IValidator GetValidator()
	    {
	        return new TestPromiseV();
	    }
	}
}
