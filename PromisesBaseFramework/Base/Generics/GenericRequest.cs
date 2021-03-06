﻿using System.Runtime.Serialization;
using FluentValidation;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    [DataContract]
    public class GenericRequest: IAmAPromiseRequest
    {
	    public GenericRequest()
	    {
		    var typeName = GetType().Name;
		    RequestName = $"{typeName.Substring(0, 1).ToLowerInvariant()}{typeName.Substring(1, typeName.Length-1)}" ;
	    }

        [DataMember(Name = "_returnLog")]
        public bool ReturnLog { get; set; }

        [DataMember(Name = "_requestId")]
        public string RequestId { get; set; }

        [DataMember(Name = "_requestName")]
        public string RequestName { get; set; }

        public virtual IValidator GetValidator()
        {
            return new GenericValidator<GenericRequest>();
        }

        
    }
}