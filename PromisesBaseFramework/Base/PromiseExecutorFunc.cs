using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
{
    public class PromiseExecutorFunc<TW, TE>
        where TW : IAmAPromiseWorkload
        where TE : IAmAPromiseResponse
    {
        public TW W { get; set; }
        public TE Rx { get; set; }
    }
}