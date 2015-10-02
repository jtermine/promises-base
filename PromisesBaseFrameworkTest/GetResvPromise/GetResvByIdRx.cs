using System.Runtime.Serialization;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.GetResvPromise
{
    [DataContract]
    public class GetResvByIdRx: GenericResponse
    {
        [DataMember(Name = "resvEntity")]
        public ResvEntity ResvEntity { get; set; }
    }
}