﻿using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Handlers
{
    public class WorkloadHandler<TC, TW, TR, TE>
        where TC: IHandlePromiseConfig
        where TW: IAmAPromiseWorkload
        where TR: IAmAPromiseRequest
        where TE: IAmAPromiseResponse
    {
        public Action<IHandlePromiseActions, TC, TW, TR, TE> Action { get; set; }
        public string HandlerName { get; set; }
        public IHandleEventMessage StartMessage { get; set; }
        public IHandleEventMessage EndMessage { get; set; }
    }
}
