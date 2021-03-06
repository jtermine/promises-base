﻿using System.Collections.Generic;
using Termine.Promises.Base.Generics;

namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPromiseResponse: IHandleEventMessage
    {
        string RequestId { get; set; }
        string ResponseId { get; set; }
        string ResponseCode { get; set; }
        string ResponseDescription { get; set; }
        bool IsSuccess { get; set; }
        List<GenericValidationFailure> ValidationFailures { get; set; }
        List<GenericPublicEventMessage> LogMessages { get; set; }
        bool IsRequestSensitive { get; set; }
        string Request { get; set; }
        string UserName { get; set; }
        string UserDisplayName { get; set; }
        string UserEmail { get; set; }
        
    }
}
