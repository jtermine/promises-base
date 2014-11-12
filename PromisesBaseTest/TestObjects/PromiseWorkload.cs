using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.TestObjects
{
    public class PromiseWorkload : IAmAPromiseWorkload<PromiseRequest, PromiseResponse>
    {
        public PromiseWorkload()
        {
            Request = new PromiseRequest();
            Response = new PromiseResponse();
        }

        public PromiseRequest Request { get; set; }
        public PromiseResponse Response { get; set; }

    }
}
