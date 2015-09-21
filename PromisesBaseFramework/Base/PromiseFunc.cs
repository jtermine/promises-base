using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
{
    public class PromiseFunc<TC, TU, TW, TR, TE>
        where TC : IHandlePromiseConfig
        where TU : IAmAPromiseUser
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
        public PromiseFunc() { }
        public PromiseFunc(IHandlePromiseActions p, TC c, TU u, TW w, TR rq, TE rx)
        {
            P = p;
            C = c;
            U = u;
            W = w;
            Rq = rq;
            Rx = rx;
        }

        public IHandlePromiseActions P { get; set; }
        public TC C { get; set; }
        public TU U { get; set; }
        public TW W { get; set; }
        public TR Rq { get; set; }
        public TE Rx { get; set; }
    }
}
