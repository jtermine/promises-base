using System;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class PromiseExecutorConfig<TXR, TXE, TXU> 
        where TXR : IAmAPromiseRequest
        where TXE : IAmAPromiseResponse
        where TXU : IAmAPromiseUser
    {
        public IAmAStrongPromiseFactory<TXR, TXE, TXU> PromiseFactory { get; set; }
        public TXR Rq { get; set; }
        public Action<TXE> OnResponse { get; set; }
        public TXU U { get; set; }
    }
}