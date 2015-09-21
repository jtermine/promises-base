using System.Collections.Generic;
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

        [DataMember(Name = "validationFailures")]
        public List<GenericValidationFailure> ValidationFailures { get; set; } = new List<GenericValidationFailure>();

        [DataMember(Name = "logMessages")]
        public List<GenericPublicEventMessage> LogMessages { get; set; } = new List<GenericPublicEventMessage>();
    }
}
