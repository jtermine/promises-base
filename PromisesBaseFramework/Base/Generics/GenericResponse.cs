using System.Collections.Generic;
using System.Runtime.Serialization;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    [DataContract]
    public class GenericResponse: IAmAPromiseResponse
    {
        [DataMember(Name = "_requestId")]
        public string RequestId { get; set; }

        [DataMember(Name = "_responseId")]
        public string ResponseId { get; set; }

        [DataMember(Name = "_responseDescription")]
        public string ResponseDescription { get; set; }

        [DataMember(Name = "_isSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "_responseCode")]
        public string ResponseCode { get; set; }

        [DataMember(Name = "_validationFailures")]
        public List<GenericValidationFailure> ValidationFailures { get; set; } = new List<GenericValidationFailure>();

        [DataMember(Name = "_logMessages")]
        public List<GenericPublicEventMessage> LogMessages { get; set; } = new List<GenericPublicEventMessage>();

        [DataMember(Name = "_isRequestSensitive")]
        public bool IsRequestSensitive { get; set; }

        [DataMember(Name = "_request")]
        public string Request { get; set; }
    }
}
