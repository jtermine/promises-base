﻿using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public delegate string WorkloadSqlConfigDelegate<in TP, in TC, in TW, in TR, in TE>(TP p, TC c, TW w, TR rq, TE rx)
        where TP: IHandlePromiseActions
        where TC : class, IHandlePromiseConfig, new()
        where TW : class, IAmAPromiseWorkload, new()
        where TR : class, IAmAPromiseRequest, new()
        where TE : class, IAmAPromiseResponse, new();


}
