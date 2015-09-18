using System.Runtime.Serialization;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    [DataContract]
    public class GenericResponse: IAmAPromiseResponse
    {
        [DataMember(Name = "responseDescription")]
        public string ResponseDescription { get; set; }

        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "responseCode")]
        public string ResponseCode { get; set; }
    }
}
