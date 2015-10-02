using System.Runtime.Serialization;
using FluentValidation;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.GetResvPromise
{
    [DataContract]
    public class GetResvByIdRq: GenericRequest
    {
        [DataMember(Name = "resvId")]
        public int ResvId { get; set; }

        public override IValidator GetValidator()
        {
            return new GetResvByIdV();
        }
    }
}