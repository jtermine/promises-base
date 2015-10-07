using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
{
    public class PromiseExecutorFunc<TW, TR, TE>
        where TW : IAmAPromiseWorkload
        where TE : IAmAPromiseResponse
        where TR: IAmAPromiseRequest
    {
        public TR Rq { get; set; }
        public TW W { get; set; }
        public TE Rx { get; set; }
    }
}