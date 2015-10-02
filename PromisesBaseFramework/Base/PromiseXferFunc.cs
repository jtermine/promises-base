using Termine.Promises.Base.Handlers;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
{
    public class PromiseXferFunc<TC, TU, TW, TR, TE> : PromiseFunc<TC, TU, TW, TR, TE> 
        where TC : IHandlePromiseConfig
        where TU : IAmAPromiseUser
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
        public WorkloadXferHandlerConfig XferConfig { get; set; } = new WorkloadXferHandlerConfig();

        public PromiseXferFunc() { }
        public PromiseXferFunc(WorkloadXferHandlerConfig xferConfig, IHandlePromiseActions p, TC c, TU u, TW w, TR rq, TE rx)
        {
            XferConfig = xferConfig;
            P = p;
            C = c;
            U = u;
            W = w;
            Rq = rq;
            Rx = rx;
        }
        
    }
}