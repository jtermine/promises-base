using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public delegate WorkloadSqlHandlerConfig WorkloadSqlHandlerConfigDelegate<in TP, in TC, in TU, in TW, in TR, in TE>(
        TP p, TC c, TU u, TW w, TR rq, TE rx)
        where TP : IHandlePromiseActions
        where TC : class, IHandlePromiseConfig, new()
        where TU : class, IAmAPromiseUser, new()
        where TW : class, IAmAPromiseWorkload, new()
        where TR : class, IAmAPromiseRequest, new()
        where TE : class, IAmAPromiseResponse, new();
}
