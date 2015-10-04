using System.Runtime.Serialization;
using FluentValidation;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.TestPromiseComponents
{
    [DataContract]
	public class TestPromiseRq : GenericRequest
	{
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "claim")]
        public string Claim { get; set; }

	    public override IValidator GetValidator()
	    {
	        return new TestPromiseV();
	    }
	}
}
