﻿using System.Runtime.Serialization;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    [DataContract]
    public class GenericPublicEventMessage: IAmAPublicEventMessage
    {
        [DataMember(Name = "eventNumber")]
        public int EventNumber { get; set; }

        [DataMember(Name = "eventPublicMessage")]
        public string EventPublicMessage { get; set; }

        [DataMember(Name = "eventPublicDetails")]
        public string EventPublicDetails { get; set; }
    }
}