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

        [DataMember(Name = "_userName")]
        public string UserName { get; set; }

        [DataMember(Name = "_userDisplayName")]
        public string UserDisplayName { get; set; }

        [DataMember(Name = "_userEmail")]
        public string UserEmail { get; set; }
        
        [DataMember(Name = "_minorEventNumber")]
        public int MinorEventNumber { get; set; }

        [DataMember(Name = "_eventNumber")]
        public int EventNumber { get; set; }

        [DataMember(Name = "_eventPublicMessage")]
        public string EventPublicMessage { get; set; }

        [DataMember(Name = "_eventPublicDetails")]
        public string EventPublicDetails { get; set; }

        [DataMember(Name = "_isSensitiveMessage")]
        public bool IsSensitiveMessage { get; set; }

        [DataMember(Name = "_isFailure")]
        public bool IsFailure { get; set; }
    }
}