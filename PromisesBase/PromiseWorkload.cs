using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public class PromiseWorkload<TT, TR> : IAmAPromiseWorkload<TT, TR>
        where TT : IAmAPromiseRequest, new() where TR : IAmAPromiseResponse, new()
    {
        public PromiseWorkload()
        {
            Request = new TT();
            Response = new TR();
        }

        public bool TerminateProcessing { get; set; }
        public TT Request { get; set; }
        public TR Response { get; set; }

    }
}
